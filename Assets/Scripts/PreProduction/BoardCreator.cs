﻿using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class BoardCreator : MonoBehaviour 
{
    #region Fields / Properties
    //para que aparescan en el inspector sin ser visibles por otros scripts (podria ser simplemente publicos... probe con publics y anda, pero bueno
    public GameObject tileViewPrefab;
    public GameObject tileSelectionIndicatorPrefab;
    public int width = 10;
    public int depth = 10;
    public int height = 8;
    public Point pos;
	public LevelData levelData;
    //Es como una lista, pero toma 2 valores y se trabaja con eso, y tiene un metodo de "busqueda" (del valor 1, mas simple por lo que entendi)
	Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
    #endregion
    //el marker lo que hace es fijarse dinamicamente si existe, la idea es que sino existe lo haga, eso ahorra tiempo de carga
    #region marker
    Transform marker
	{
		get
		{
			if (_marker == null)
			{
				GameObject instance = Instantiate(tileSelectionIndicatorPrefab) as GameObject;
				_marker = instance.transform;
			}
			return _marker;
		}
	}
	Transform _marker;
    #endregion

    #region Public
    //estos 2 para crear cosas no random (si, hay que usar esto para mapas importantes basicamente)
    public void Grow()
	{
		GrowSingle(pos);
	}
	
	public void Shrink()
	{
		ShrinkSingle(pos);
	}
    //randomiza los 4 puntos y 
	public void GrowArea()
	{
		Rect r = RandomRect();
		GrowRect(r);
	}
    public void GrowGlobal()
    {
        Rect r = GlobalRect();
        GrowRect(r);
    }
	
	public void ShrinkArea()
	{
		Rect r = RandomRect();
		ShrinkRect(r);
	}
    public void ShrinkGlobal()
    {
        Rect r = GlobalRect();
        ShrinkRect(r);
    }
    //updatea el selector indicator
    public void UpdateMarker()
	{
		Tile t = tiles.ContainsKey(pos) ? tiles[pos] : null;
		marker.localPosition = t != null ? t.center : new Vector3(pos.x, 0, pos.y);
	}
    //lo limpia... genial para pasar de niveles o simplemente reiniciar y cargarlo de nuevo
	public void Clear()
	{
		for (int i = transform.childCount - 1; i >= 0; --i)
			DestroyImmediate(transform.GetChild(i).gameObject);
		tiles.Clear(); //no se bien xq se volveria a llamar... Creo que por si esta llamado mal? pero no haria un bucle infinito AAAAAA
	}
    #endregion
    //un QUILOMBO, ver cada cosa bien despues
    #region save/load
    public void Save()
	{
        string filePath = Application.dataPath + "/Resources/Levels";
        //Crea la carpeta/lugar
        if (!Directory.Exists(filePath))
             CreateSaveDirectory();
        
        LevelData board = ScriptableObject.CreateInstance<LevelData>();
        board.tiles = new List<Vector3>(tiles.Count);
        //Aca se podria poner que tambien te guarde el tipo de material
        foreach (Tile t in tiles.Values)
           board.tiles.Add(new Vector3(t.pos.x, t.height, t.pos.y));
        
        string fileName = string.Format("Assets/Resources/Levels/{1}.asset", filePath, name);
        AssetDatabase.CreateAsset(board, fileName);
    }

    void CreateSaveDirectory()
    {
        string filePath = Application.dataPath + "/Resources";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets", "Resources");
        filePath += "/Levels";
        if (!Directory.Exists(filePath))
            AssetDatabase.CreateFolder("Assets/Resources", "Levels");
        AssetDatabase.Refresh();
    }

    public void Load()
	{
		Clear();
		if (levelData == null)
			return;
		
		foreach (Vector3 v in levelData.tiles)
		{
			Tile t = Create();
			t.Load(v);
			tiles.Add(t.pos, t);
		}
	}
	#endregion

	#region Private
    //todo esto es simplemente para que devuelva 4 valores principalmente por la altura no?
	Rect RandomRect()
	{
        int x = Random.Range(0, width);
        int y = Random.Range(0, depth);
        int w = Random.Range(1, width - x + 1);
        int h = Random.Range(1, depth - y + 1);
        return new Rect(x, y, w, h);
	}
    Rect GlobalRect()
    {
        int x = 0;
        int y = 0;
        int w = width;
        int h = depth;
        return new Rect(x, y, w, h);
    }


    void GrowRect(Rect rect)
	{
		for (int y = (int) rect.yMin; y<(int)rect.yMax; ++y)
		{
			for (int x = (int) rect.xMin; x<(int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				GrowSingle(p);
			}
		}
}
	
	void ShrinkRect(Rect rect)
	{
		for (int y = (int) rect.yMin; y<(int)rect.yMax; ++y)
		{
			for (int x = (int) rect.xMin; x<(int)rect.xMax; ++x)
			{
				Point p = new Point(x, y);
				ShrinkSingle(p);
			}
		}
	}
    //Crea el tile 
	Tile Create()
	{
		GameObject instance = Instantiate(tileViewPrefab) as GameObject;
		instance.transform.parent = transform;
		return instance.GetComponent<Tile>();
	}
	//si tiene punto lo devuelte, sino crea uno
	Tile GetOrCreate(Point p)
	{
		if (tiles.ContainsKey(p))
			return tiles[p];
		
		Tile t = Create();
		t.Load(p, 0);
		tiles.Add(p, t);
		
		return t;
	}
	
	void GrowSingle(Point p)
	{
		Tile t = GetOrCreate(p);
		if (t.height<height)
			t.Grow();
	}
    //este no crea si no existe, y si es mas bajo que 0 destruye el tile
	void ShrinkSingle(Point p)
	{
		if (!tiles.ContainsKey(p))
			return;
		
		Tile t = tiles[p];
		t.Shrink();
		
		if (t.height <= 0)
		{
			tiles.Remove(p);
			DestroyImmediate(t.gameObject);
		}
	}

	
	#endregion
}
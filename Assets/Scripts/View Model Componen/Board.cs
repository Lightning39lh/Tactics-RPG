using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Board : MonoBehaviour
{
	#region Fields / Properties
 	public GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile>();
	private Point[] dirs = new Point[4] 
   {
		new Point(0, 1),
		new Point(0, -1),
		new Point(1, 0),
		new Point(-1, 0)
	};
    //for abilitys uses
    public Point min { get { return _min; } }
    public Point max { get { return _max; } }
    Point _min;
    Point _max;
    #endregion
    //see colors/highlights
    #region highlights
    Color selectedTileColor = new Color(0, 1, 1, 1);
    Color defaultTileColor = new Color(1, 1, 1, 1);
    public void SelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", selectedTileColor);
    }

    public void DeSelectTiles(List<Tile> tiles)
    {
        for (int i = tiles.Count - 1; i >= 0; --i)
            tiles[i].GetComponent<Renderer>().material.SetColor("_Color", defaultTileColor);
    }
    #endregion
    #region Public
    
    public void Load(LevelData data)
    {
        _min = new Point(int.MaxValue, int.MaxValue);
        _max = new Point(int.MinValue, int.MinValue);
        for (int i = 0; i < data.tiles.Count; ++i)
        {
            GameObject instance = Instantiate(tilePrefab) as GameObject;
            Tile t = instance.GetComponent<Tile>();
            t.Load(data.tiles[i]);
            tiles.Add(t.pos, t);
            _min.x = Mathf.Min(_min.x, t.pos.x);
            _min.y = Mathf.Min(_min.y, t.pos.y);
            _max.x = Mathf.Max(_max.x, t.pos.x);
            _max.y = Mathf.Max(_max.y, t.pos.y);
        }
    }

    public Tile GetTile(Point p)
	{
		return tiles.ContainsKey(p)? tiles[p] : null; //if contains return the point, else null cause can have nulled tiles 
	}

	public List<Tile> Search(Tile start, Func<Tile, Tile, bool> addTile) //the second part (addTile) is a func that return bool?
	{
		List<Tile> retValue = new List<Tile>();
		retValue.Add(start);

		ClearSearch();
		Queue<Tile> checkNext = new Queue<Tile>(); 
		Queue<Tile> checkNow = new Queue<Tile>();

		start.distance = 0;
		checkNow.Enqueue(start); //enqueue = poner/put/set

		while (checkNow.Count > 0)
		{
			Tile t = checkNow.Dequeue(); //dequeue = sacar/take/get
			for (int i = 0; i< 4; ++i)
			{
				Tile next = GetTile(t.pos + dirs[i]);
				if (next == null || next.distance <= t.distance + 1)
					continue; //if haven't next or is one of the above, continue to the next i of the for (we can add here the blocks)

				if (addTile(t, next)) //if pass we enqueue and do the same...
				{
					next.distance = t.distance + 1;
					next.prev = t;
					checkNext.Enqueue(next);
					retValue.Add(next);
				}
			}

			if (checkNow.Count == 0) //after check the 4 points, swap to the next
				SwapReference(ref checkNow, ref checkNext);
		}

		return retValue;
	}

	
#endregion

    #region Private
    private void ClearSearch() //format all the values (to 0) and save the max distance for searching a more efficient path
{
		foreach (Tile t in tiles.Values)
		{
			t.prev = null;
			t.distance = int.MaxValue;
		}
	}

	void SwapReference(ref Queue<Tile> a, ref Queue<Tile> b)
	{
		Queue<Tile> temp = a;
		a = b;
		b = temp;
    }
	#endregion
 }
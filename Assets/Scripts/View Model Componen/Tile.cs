using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    //used for grow that step...
    #region Const

    public const float stepHeight = 0.25f;
    #endregion
    
    //el punto creado antes, la altura (si es que vamos a usar) y el centro para despues colocar las cosas)
    #region Fields / Properties
    public Point pos;
	public int height;
	public Vector3 center { get { return new Vector3(pos.x, height * stepHeight, pos.y); } }
    public GameObject content;
    [HideInInspector] public Tile prev; //put the mouse in HideInInspector for more info
    [HideInInspector] public int distance;
    #endregion
    //porque la idea de EL JUEGO DEL TUTORIAL es que se pueda subir y bajar aleatoriamente en la creacion de mapa (igual sirve para subirlo y bajarlo en caso de que querramos hacerlo)
    #region Up/Down
    public void Grow()
	{
		height++;
		Match();
	}
	
	public void Shrink()
	{
		height--;
		Match();
	}
    #endregion
    //para cargar los tiles como vector 3, o como un "punto, altura"
    #region Overcharged Load
    public void Load(Point p, int h)
	{
		pos = p;
		height = h;
		Match();
	}
	
	public void Load(Vector3 v)
	{
		Load(new Point((int) v.x, (int) v.z), (int) v.y);
	}
    #endregion
    //para encontrar donde esta el Tile si modifico la altura o algo
    #region Private
    void Match()
    {
        transform.localPosition = new Vector3(pos.x, height * stepHeight / 2f, pos.y);
        transform.localScale = new Vector3(1, height * stepHeight, 1);
    }
    #endregion
}
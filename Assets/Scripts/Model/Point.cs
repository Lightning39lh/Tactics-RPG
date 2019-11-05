
using System.Collections;
using System;
using UnityEngine;
[System.Serializable] //The Serializable attribute lets you embed a class with sub properties in the inspector.

//You can use this to display variables in the inspector similar to how a Vector3 shows up in the inspector.

public struct Point : IEquatable<Point>
{
    //ESTO DE REGION ES RE CHETO basicamente es para que cuando le pones el menos tenga el nombre

	#region Fields
	public int x;
	public int y;
	#endregion

	#region Constructors
	public Point(int x, int y)
	{
		this.x = x;
		this.y = y;
	}
	#endregion

	#region Operator Overloads 
	public static Point operator +(Point p1, Point p2)
	{
		return new Point(p1.x + p2.x, p1.y + p2.y);
	}
	
	public static Point operator -(Point p1, Point p2)
	{
		return new Point(p1.x - p2.x, p1.y - p2.y);
	}
	
	public static bool operator ==(Point a, Point b) //p1/p2
	{
		return a.x == b.x && a.y == b.y;
	}
    public static implicit operator Vector2(Point p)
    {
        return new Vector2(p.x, p.y);
    }
    public static bool operator !=(Point a, Point b) //p1/p2
    {
		return !(a == b);
	}
	#endregion
    //sobrecargo todo para despues trabajar RE TRANQUILO
	#region Object Overloads
	public override bool Equals(object obj)
	{
		if (obj is Point)
		{
			Point p = (Point)obj;
			return x == p.x && y == p.y;
		}
		return false;
	}
	
	public bool Equals(Point p)
	{
		return x == p.x && y == p.y;
	}
	
	public override int GetHashCode() //devuelve la suma de los puntos
    {
		return x ^ y;
	}
    
	public override string ToString() //para imprimir directamente asi el punto (para testear el debuger)
	{
		return string.Format("({0},{1})", x, y);
	}
	#endregion
}


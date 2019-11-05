﻿using UnityEngine;
using System;
using System.Collections;

//para ver si esta manteniendo el eje o no (y tiene que seguir moviendose o simplemente mover 1)
class Repeater
{

	const float threshold = 0.5f;//tiempo que hay que mantener
	const float rate = 0.25f; //velocidad que se repite
	float _next;
	bool _hold;
	string _axis;
	
	public Repeater(string axisName) //constructor para funcionar con el nombre del eje que le mandas despues
	{
		_axis = axisName;
	}
	
	public int Update()
	{

		int retValue = 0;//returned value
		int value = Mathf.RoundToInt(Input.GetAxisRaw(_axis));
		
		if (value != 0)
		{
			if (Time.time > _next)
			{
				retValue = value;
				_next = Time.time + (_hold? rate : threshold); //odio tener que admitir que me tengo que acostumbrar a esta nomeclatura... (si hold es verdadera , rate, sino threshold)
				_hold = true;
			}
		}
		else
		{
			_hold = false;
			_next = 0;
		}
		
		return retValue;
	}
}

public class InputController : MonoBehaviour 
{
    //bueno, ver bien eventos xq es la forma en la que se relacionan diversos scripts con este (en este caso con el control)
	public static event EventHandler<InfoEventArgs<Point>> moveEvent; //pass the "Point"
	public static event EventHandler<InfoEventArgs<int>> fireEvent; //pass the int for "fire1/2/3?"

	Repeater _hor = new Repeater("Horizontal");
	Repeater _ver = new Repeater("Vertical");
	string[] _buttons = new string[] { "Fire1", "Fire2", "Fire3" };
	void Update()
	{
		int x = _hor.Update();
		int y = _ver.Update();
		if (x != 0 || y != 0)
		{
			if (moveEvent != null)
            {
                moveEvent(this, new InfoEventArgs<Point>(new Point(x, y)));
            }
        }

		for (int i = 0; i< 3; ++i)
		{
			if (Input.GetButtonUp(_buttons[i]))
			{
				if (fireEvent != null)
					fireEvent(this, new InfoEventArgs<int>(i));
        	}
		}
	}
}
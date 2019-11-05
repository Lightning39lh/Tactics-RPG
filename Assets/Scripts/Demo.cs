using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo : MonoBehaviour
{
    void OnEnable()
    {
        InputController.moveEvent += OnMoveEvent;
        InputController.fireEvent += OnFireEvent;
    }

   
    
    void OnDisable()
    {
        InputController.moveEvent -= OnMoveEvent; //you can create the metodh with right click! (in the hint)
        InputController.fireEvent -= OnFireEvent;
    }

    void OnMoveEvent(object sender, InfoEventArgs<Point> e)
    {
        Debug.Log("Move " + e.info.ToString());
    }

    void OnFireEvent(object sender, InfoEventArgs<int> e)
	{
		Debug.Log("Fire " + e.info);
	}
}

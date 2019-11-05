using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class InfoEventArgs<T> : EventArgs
//can hold a single field of any data type named info
{
    public T info;

    public InfoEventArgs()
    {
        info = default(T);
    }

    public InfoEventArgs(T info)
    {
        this.info = info;
    }
}
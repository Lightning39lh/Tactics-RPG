using UnityEngine;
using System.Collections;
public abstract class Modifier
{
    //the idea is check all the modifiers, and order for the calculous
    public readonly int sortOrder;
    public Modifier(int sortOrder)
    {
        this.sortOrder = sortOrder; 
    }
}
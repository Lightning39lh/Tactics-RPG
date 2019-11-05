using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject
{
    //para representar el campo solo se necesita ubicaciones de los tiles... asi que simplemente le meto una lista de Vector 3 
    public List<Vector3> tiles;
}

using UnityEngine;
using System.Collections;
public class FacingIndicator : MonoBehaviour
{
    [SerializeField] SpriteRenderer[] directions;
    [SerializeField] Color selected;
    [SerializeField] Color normal;

    public void SetDirection(Directions dir)
    {
        int index = (int)dir;
        for (int i = 0; i < 4; ++i)
            directions[i].color = (i == index) ? selected : normal;
    }
}
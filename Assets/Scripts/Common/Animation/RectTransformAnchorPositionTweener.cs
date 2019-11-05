using UnityEngine;
using System.Collections;

//maintains a reference to its own RectTransform and sets the interpolated Vector (from the update loop) as the anchoredPosition value
public class RectTransformAnchorPositionTweener : Vector3Tweener
{
    RectTransform rt;
    void Awake()
    {
       
        rt = transform as RectTransform;
    }
    protected override void OnUpdate()
    {
        base.OnUpdate();
        rt.anchoredPosition = currentTweenValue;
    }
}
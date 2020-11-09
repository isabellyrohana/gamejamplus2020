using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
    
    public static float CurveAsc(float time) => Mathf.Cos(Mathf.Lerp(Mathf.PI, Mathf.PI * 1.5f, time)) + 1f;

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomWait : MonoBehaviour
{
    
    public static IEnumerator Wait(float time = 2f, Action callback = null)
    {
        yield return new WaitForSeconds(time);
        callback?.Invoke();
    }

}

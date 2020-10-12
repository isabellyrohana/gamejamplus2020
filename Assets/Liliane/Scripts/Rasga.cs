using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rasga : MonoBehaviour
{
    private bool canSound;

    void OnBecameVisible()
    {
        canSound = true;
    }

    private void OnBecameInvisible() 
    {
        canSound = false;
    }

    private void PlaySfx(int idSound)
    {
        if(canSound)
        {
            SoundFxController.Instance.playFx(idSound);
        }  
    }
}

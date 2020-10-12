using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthsScene : MonoBehaviour
{
    public GameObject manpiguard;
    public Transform friend;

    public void ActiveManpiguard()
    {
        manpiguard.SetActive(true);
    }

}

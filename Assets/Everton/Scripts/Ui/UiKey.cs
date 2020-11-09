using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiKey : MonoBehaviour
{
    
    [SerializeField] private Image keyImage = null;

    private void Awake() {
        Events.ObserverManager.Register(NotifyEvent.Interactions.Key.Show, GetKey);
    }

    private void GetKey()
    {
        keyImage.color = Color.yellow;
    }

}

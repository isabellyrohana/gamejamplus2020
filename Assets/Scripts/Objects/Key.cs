using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Key : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other) 
    {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            SoundFxController.Instance.playFx(8);
            PlayerController.Instance.GetKey();
            Events.ObserverManager.Notify(NotifyEvent.Interactions.Key.Show);
            Destroy(gameObject);
        }
        
    }

}

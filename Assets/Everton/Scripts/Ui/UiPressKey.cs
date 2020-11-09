using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UiPressKey : MonoBehaviour
{

    [SerializeField] private string key;
    
    private Image imageKey;
    private Text textKey;
    
    void Awake() 
    {

        imageKey = GetComponent<Image>();
        textKey = GetComponentInChildren<Text>();

        imageKey.gameObject.SetActive(false);

        Events.ObserverManager.Register<string, Vector2>(NotifyEvent.Interactions.Key.Show, ShowKey);
        Events.ObserverManager.Register<string>(NotifyEvent.Interactions.Key.Hide, HideKey);
    }

    private void ShowKey(string key, Vector2 position)
    {
        if (!string.IsNullOrEmpty(key) && key.Equals(this.key))
        {
            textKey.text = this.key;
            imageKey.rectTransform.position = position;
            imageKey.gameObject.SetActive(true);
        }
    }

    private void HideKey(string key)
    {
        if (!string.IsNullOrEmpty(key) && key.Equals(this.key))
        {
            imageKey.gameObject.SetActive(false);
        }
    }

}

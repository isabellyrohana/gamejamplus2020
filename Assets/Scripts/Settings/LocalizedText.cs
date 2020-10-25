using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LocalizedText : MonoBehaviour
{

    [SerializeField] private bool isSettings = false;

    private Text text = null;

    public string key;

    private void Awake() {
        text = GetComponent<Text>();
        if (isSettings) Events.ObserverManager.Register(NotifyEvent.Language.ChangeInSettings, LanguageChangeEvent);
        Events.ObserverManager.Register(NotifyEvent.Language.Change, LanguageChangeEvent);
    }

    void Start() 
    {
        LanguageChangeEvent();
    }

    private void LanguageChangeEvent()
    {
        if (text != null) text.text = LocalizationManager.Instance.GetLocalizationValue(key);
    }

}

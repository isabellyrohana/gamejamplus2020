using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UiGenericMenu : MonoBehaviour
{
    
    [SerializeField] private Button buttonClose;
    [SerializeField] private UiFadeEffect bgFadeEffect;
    private AudioSource _audioSource;

    protected void Awake() {
        _audioSource = GetComponent<AudioSource>();

        buttonClose?.onClick.RemoveAllListeners();
        buttonClose?.onClick.AddListener(() => ButtonClose());
    }

    public void Show(Action callback = null) 
    {
        _audioSource?.Play();
        bgFadeEffect.FadeIn(callback);
    }

    public void ButtonClose(Action callback = null) 
    {
        _audioSource?.Play();
        bgFadeEffect.FadeOut(callback);
    }

}

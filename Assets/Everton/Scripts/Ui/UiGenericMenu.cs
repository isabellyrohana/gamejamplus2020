using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public abstract class UiGenericMenu : MonoBehaviour
{
    
    [SerializeField] private UiMainMenuController uiMainMenuController;
    [SerializeField] private Button buttonClose;
    [SerializeField] private UiFadeEffect bgFadeEffect;
    private AudioSource _audioSource;

    void Awake() {
        _audioSource = GetComponent<AudioSource>();

        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(ButtonClose);
    }

    public void Show() 
    {
        _audioSource.Play();
        bgFadeEffect.FadeIn();
    }

    private void ButtonClose() 
    {
        _audioSource.Play();
        bgFadeEffect.FadeOut();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(AudioSource))]
public abstract class UiGenericMenu : MonoBehaviour
{

    private static List<UiGenericMenu> menus = new List<UiGenericMenu>();

    [SerializeField] private Button buttonClose = null;
    [SerializeField] private UiFadeEffect bgFadeEffect = null;
    private AudioSource _audioSource = null;

    private bool _isShowing = false;

    protected void Awake() {
        _audioSource = GetComponent<AudioSource>();

        buttonClose?.onClick.RemoveAllListeners();
        buttonClose?.onClick.AddListener(() => ButtonClose());
    }

    public abstract void Setup();

    void Update()
    {
        /*if (_isShowing && Input.GetButtonDown("Pause"))
        {
            if (menus.Count > 0) menus[menus.Count - 1].Hide();
        }*/
    }

    public void Show(Action callback = null)
    {
        if (!_isShowing)
        {
            _audioSource?.Play();
            Setup();
            bgFadeEffect.FadeIn(() =>
            {
                callback?.Invoke();
                _isShowing = true;
                menus.Add(this);
            });
        }
    }

    public void Hide(Action callback = null)
    {
        if (_isShowing)
        {
            _audioSource?.Play();
            bgFadeEffect.FadeOut(() =>
            {
                callback?.Invoke();
                _isShowing = false;
                menus.Remove(this);
                if (menus.Count <= 0) PlayerController.Instance?.Pause(false);
            });
        }
    }

    public void ButtonClose(Action callback = null) => Hide(callback);

    public bool IsShowing() => _isShowing;

    private void OnPause()
    {
        if (_isShowing)
        {
            if (menus.Count > 0) menus[menus.Count - 1].Hide();
        }
    }
    
    public void PlaySfxSound() => _audioSource?.Play();

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UiMainMenuController : MonoBehaviour
{

    [Header("Input System Ui Input Module")]
    [SerializeField] private InputSystemUIInputModule uiInputModule;

    [Header("Audios")]
    [SerializeField] private AudioClip doorOpenAudio;
    [SerializeField] private AudioClip pressButtonAudio;

    [Header("Background")]
    [SerializeField] private UiFadeEffect fadeEffect;

    [Header("Menus")]
    [SerializeField] private UiControlsController uiControlsController;
    [SerializeField] private UiCreditsController uiCreditsController;
    [SerializeField] private UiSettingsController uiSettingsController;

    [Header("Animation")]
    [SerializeField] private Animator doorAnimator;

    private AudioSource _audioSource;

    void Awake()
    {
        uiInputModule.enabled = false;
        doorAnimator.SetBool("DoorOpen", false);

        _audioSource = GetComponent<AudioSource>();
        fadeEffect.GetComponent<CanvasGroup>().alpha = 1f;
        StartCoroutine(CustomWait.Wait(1f, () => fadeEffect.FadeOut(() => uiInputModule.enabled = true)));
    }

    public void ButtonPlay()
    {
        uiInputModule.enabled = false;
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = doorOpenAudio;
        _audioSource.Play();
        doorAnimator.SetBool("DoorOpen", true);

        StartCoroutine(CustomWait.Wait(50f / 60f, () => fadeEffect.FadeIn(() => SceneController.ToPreGameScreen())));
    }

    public void ButtonExitGame()
    {
        uiInputModule.enabled = false;
        ButtonAudioPlay();
        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            while(_audioSource.clip != null && _audioSource.isPlaying) yield return null;
            SceneController.Exit();
        }
    }

    private void ButtonAudioPlay()
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = pressButtonAudio;
        _audioSource.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UiMainMenuController : MonoBehaviour
{

    [Header("Audios")]
    [SerializeField] private AudioClip doorOpenAudio;
    [SerializeField] private AudioClip pressButtonAudio;

    [Header("Buttons")]
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonControls;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExitGame;
    [SerializeField] private Button buttonSettings;

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
        doorAnimator.SetBool("DoorOpen", false);

        _audioSource = GetComponent<AudioSource>();
        fadeEffect.GetComponent<CanvasGroup>().alpha = 1f;
        StartCoroutine(CustomWait.Wait(1f, () => fadeEffect.FadeOut()));

        buttonPlay.onClick.RemoveAllListeners();
        buttonControls.onClick.RemoveAllListeners();
        buttonCredits.onClick.RemoveAllListeners();
        buttonExitGame.onClick.RemoveAllListeners();
        buttonSettings.onClick.RemoveAllListeners();

        buttonPlay.onClick.AddListener(ButtonPlay);
        buttonControls.onClick.AddListener(ButtonControls);
        buttonCredits.onClick.AddListener(ButtonCredits);
        buttonExitGame.onClick.AddListener(ButtonExitGame);
        buttonSettings.onClick.AddListener(ButtonSettings);
    }

    private void ButtonPlay() 
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = doorOpenAudio;
        _audioSource.Play();
        doorAnimator.SetBool("DoorOpen", true);
        StartCoroutine(CustomWait.Wait(50f/60f, Play));
    }

    public void Play()
    {
        fadeEffect.FadeIn(() => SceneController.ToPreGameScreen());
    }

    private void ButtonControls()
    {
        ButtonAudioPlay();
        uiControlsController.Show();
    }

    private void ButtonCredits()
    {
        ButtonAudioPlay();
        uiCreditsController.Show();
    }

    private void ButtonExitGame()
    {
        ButtonAudioPlay();
        SceneController.Exit();
    }

    private void ButtonSettings()
    {
        ButtonAudioPlay();
        uiSettingsController.Show();
    }

    private void ButtonAudioPlay()
    {
        if (_audioSource.isPlaying) _audioSource.Stop();
        _audioSource.clip = pressButtonAudio;
        _audioSource.Play();
    }

}

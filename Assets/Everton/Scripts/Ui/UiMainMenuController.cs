using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class UiMainMenuController : MonoBehaviour
{

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

    private AudioSource _audioSource;

    void Awake()
    {
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
        _audioSource.Play();
        fadeEffect.FadeIn(() => {
            SceneController.ToGame();
            MainMusicController.Instance.PlayGameAudioClip();
        });
    }

    private void ButtonControls() => uiControlsController.Show();
    private void ButtonCredits() => uiCreditsController.Show();
    private void ButtonExitGame() => SceneController.Exit();
    private void ButtonSettings() => uiSettingsController.Show();

}

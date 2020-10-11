using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiMainMenuController : MonoBehaviour
{

    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonControls;
    [SerializeField] private Button buttonCredits;
    [SerializeField] private Button buttonExitGame;
    [SerializeField] private Button buttonSettings;

    [SerializeField] private UiFadeEffect fadeEffect;

    void Awake()
    {
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

    private void ButtonPlay() => fadeEffect.FadeIn(() => {
        SceneController.ToGame();
        MainMusicController.Instance.PlayGameAudioClip();
    });

    private void ButtonControls()
    {
        Debug.Log("Button Controls");
    }

    private void ButtonCredits()
    {
        Debug.Log("Button Credits");
    }

    private void ButtonExitGame() => SceneController.Exit();

    private void ButtonSettings()
    {
        Debug.Log("Button Settings");
    }

}

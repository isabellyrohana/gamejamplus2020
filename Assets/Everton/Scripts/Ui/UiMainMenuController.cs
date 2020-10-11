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

    void Awake() {
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

    private void ButtonPlay() => SceneController.ToGame();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseController : MonoBehaviour
{

    [SerializeField] private UiPauseMenuController uiPauseMenuController;

    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonControls;
    [SerializeField] private Button buttonJournals;
    [SerializeField] private Button buttonExitGame;

    private UiFadeEffect _pauseMenuFadeEffect;

    void Awake() {
        buttonContinue.onClick.RemoveAllListeners();
        buttonControls.onClick.RemoveAllListeners();
        buttonJournals.onClick.RemoveAllListeners();
        buttonExitGame.onClick.RemoveAllListeners();

        buttonContinue.onClick.AddListener(() => uiPauseMenuController.ClosePauseMenu());
        buttonControls.onClick.AddListener(() => {});
        buttonJournals.onClick.AddListener(() => uiPauseMenuController.StartJournalsMenu());
        buttonExitGame.onClick.AddListener(() => uiPauseMenuController.ClosePauseMenu(() => SceneController.ToMainMenu()));
    }

}

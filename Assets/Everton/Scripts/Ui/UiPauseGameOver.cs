using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseGameOver : UiGenericMenu
{
    
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;

    protected new virtual void Awake() {
        buttonRestart.onClick.RemoveAllListeners();
        buttonExit.onClick.RemoveAllListeners();

        buttonRestart.onClick.AddListener(Restart);
        buttonExit.onClick.AddListener(ButtonExitGame);
    }

    public override void Setup()
    {
        
    }

    private void ButtonExitGame() => SceneController.ToMainMenu();

    public void Restart() => SceneController.ToReloadScreen();

}

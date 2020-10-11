using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiJournalsController : MonoBehaviour
{

    [SerializeField] UiPauseMenuController uiPauseMenuController;
    
    [SerializeField] private Button buttonClose;

    void Awake() {
        buttonClose.onClick.RemoveAllListeners();
        buttonClose.onClick.AddListener(() => uiPauseMenuController.CloseJournalsMenu());
    }

}

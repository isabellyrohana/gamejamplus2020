using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiCreditsController : MonoBehaviour
{

    [SerializeField] private UiMainMenuController uiMainMenuController;
    [SerializeField] private Button buttonClose;
    [SerializeField] private UiFadeEffect bgFadeEffect;

    void Awake() {
        buttonClose.onClick.RemoveAllListeners();

        buttonClose.onClick.AddListener(ButtonClose);
    }

    public void Show() => bgFadeEffect.FadeIn();

    private void ButtonClose() => bgFadeEffect.FadeOut();

}

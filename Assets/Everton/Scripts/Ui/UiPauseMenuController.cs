using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseMenuController : MonoBehaviour
{

    [SerializeField] private Button buttonPause;

    [SerializeField] private UiFadeEffect bgFadeEffect;
    
    [SerializeField] private UiFadeEffect pauseMenu;
    [SerializeField] private UiFadeEffect journalsMenu;
    [SerializeField] private UiFadeEffect journalMenu;

    void Awake() {
        buttonPause.onClick.RemoveAllListeners();
        buttonPause.onClick.AddListener(StartPauseMenu);
        buttonPause.GetComponent<UiFadeEffect>().FadeIn();
    }

    public void StartPauseMenu() 
    {
        buttonPause.GetComponent<UiFadeEffect>().FadeOut();
        bgFadeEffect.FadeIn();
        Transite(null, pauseMenu);
    }

    public void ClosePauseMenu(Action callback = null)
    {
        buttonPause.GetComponent<UiFadeEffect>().FadeIn();
        bgFadeEffect.FadeOut();
        Transite(pauseMenu, null, callback);
    }

    public void StartJournalsMenu() => Transite(pauseMenu, journalsMenu);
    public void CloseJournalsMenu() => Transite(journalsMenu, pauseMenu);
    public void StartJournal() => Transite(journalsMenu, journalMenu);
    public void CloseJournal() => Transite(journalMenu, journalsMenu);

    private void Transite(UiFadeEffect from, UiFadeEffect to, Action callback = null) 
    {
        if (from != null) from.FadeOut(() => {
            callback?.Invoke();
            to?.FadeIn();
        });
        else 
        {
            to?.FadeIn(callback);
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseController : UiGenericMenu
{

    [Header("Buttons")]
    [SerializeField] private Button buttonContinue;
    [SerializeField] private Button buttonControls;
    [SerializeField] private Button buttonJournals;
    [SerializeField] private Button buttonExitGame;

    [Header("Slot Item")]
    [SerializeField] private Image item;

    [Header("Menus")]
    [SerializeField] private UiPauseControls uiPauseControls;
    [SerializeField] private UiPauseJournals uiPauseJournals;
    [SerializeField] private UiPauseJournal uiPauseJournal;
    [SerializeField] private UiPauseGameOver uiPauseGameOver;

    protected virtual new void Awake() {
        base.Awake();
        
        buttonContinue.onClick.RemoveAllListeners();
        buttonControls.onClick.RemoveAllListeners();
        buttonJournals.onClick.RemoveAllListeners();
        buttonExitGame.onClick.RemoveAllListeners();

        buttonContinue.onClick.AddListener(ButtonContinue);
        buttonControls.onClick.AddListener(ButtonControls);
        buttonJournals.onClick.AddListener(ButtonJournals);
        buttonExitGame.onClick.AddListener(ButtonExitGame);
    }

    public override void Setup()
    {
        
    }

    private void ButtonContinue() => this.ButtonClose(() => PlayerController.Instance?.Pause(false));

    private void ButtonControls() => uiPauseControls.Show();

    public void ButtonJournals() => uiPauseJournals.Show();

    private void ButtonExitGame() => this.ButtonClose(() => SceneController.ToMainMenu());

    public void SetItem(Sprite image, Color color)
    {
        item.sprite = image;
        item.color = color != null ? color : Color.yellow;
    }

    public void OpenJournal(int index) 
    {
        Journal journal = Journals.GetJournal(index);
        uiPauseJournal.Setup(journal.text);
        uiPauseJournal.Show();
    }

    public void OpenLastJournal()
    {
        Journal journal = Journals.GetJournal(Journals.GetJournals().Count - 1);
        uiPauseJournal.Setup(journal.text);
        uiPauseJournal.Show();
    }

    public void OpenPicture()
    {
        uiPauseJournal.Setup("");
        uiPauseJournal.Show();
    }

    public bool JournalIsOpen() => uiPauseJournal.IsShowing();

    public void CloseJournal() => uiPauseJournal.Hide();

    public void OpenGameOver()
    {
        uiPauseGameOver.Show();
    }

}

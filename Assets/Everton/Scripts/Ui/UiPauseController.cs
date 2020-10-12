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

    protected new virtual void Awake() {
        base.Awake();
        
        buttonContinue.onClick.RemoveAllListeners();
        buttonControls.onClick.RemoveAllListeners();
        buttonJournals.onClick.RemoveAllListeners();
        buttonExitGame.onClick.RemoveAllListeners();

        buttonContinue.onClick.AddListener(ButtonContinue);
        buttonControls.onClick.AddListener(ButtonControls);
        buttonJournals.onClick.AddListener(ButtonJournals);
        buttonExitGame.onClick.AddListener(ButtonExitGame);

        Journal journal1 = new Journal("Informações do diário 1");
        Journal journal2 = new Journal("Informações do diário 2");
        Journal journal3 = new Journal("Informações do diário 3");
        Journal journal4 = new Journal("Informações do diário 4");
        Journal journal5 = new Journal("Informações do diário 5");
        Journals.AddJournal(journal1);
        Journals.AddJournal(journal2);
        Journals.AddJournal(journal3);
        Journals.AddJournal(journal4);
        Journals.AddJournal(journal5);
    }

    private void ButtonContinue() => this.ButtonClose(() => PlayerController.Instance.Pause());

    private void ButtonControls() => uiPauseControls.Show();

    public void ButtonJournals() => uiPauseJournals.Setup();

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
    }

    public void OpenLastJournal()
    {
        Journal journal = Journals.GetJournal(Journals.GetJournals().Count - 1);
        uiPauseJournal.Setup(journal.text);
    }

    public void OpenGameOver()
    {
        uiPauseGameOver.Show();
    }

}

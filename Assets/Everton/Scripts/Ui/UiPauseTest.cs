using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseTest : MonoBehaviour
{

    [SerializeField] private UiPauseController uiPauseController;
    
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonJournals;
    [SerializeField] private Button buttonLastJournal;

    void Awake() {
        buttonPause.onClick.RemoveAllListeners();
        buttonJournals.onClick.RemoveAllListeners();
        buttonLastJournal.onClick.RemoveAllListeners();

        buttonPause.onClick.AddListener(() => {
            // Deve executar ações de pausar o jogo e então mostrar o menu de pause
            uiPauseController.Show();
        });
        buttonJournals.onClick.AddListener(() => {
            // Deve executar ações de pausar o jogo e chamar a abertura do painel dos diários
            uiPauseController.ButtonJournals();
        });
        buttonLastJournal.onClick.AddListener(() => {
            // Deve executar ações de pausar o jogo e adicionar um diário
            Journal journal = new Journal("Texto do diário"); // Essa informações de texto deve ficar contida no próprio diário
            Journals.AddJournal(journal);

            // Chama a abertura do último diário.
            uiPauseController.OpenLastJournal();
        });
    }

}

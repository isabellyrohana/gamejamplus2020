using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseJournals : UiGenericMenu
{

    [SerializeField] private Transform gridTransform;
    [SerializeField] private GameObject gridItemPrefab;
    [SerializeField] private UiPauseJournal uiPauseJournal;

    public override void Setup()
    {
        foreach(Transform child in gridTransform) 
        {
            Destroy(child);
            gridTransform.DetachChildren();
        }
        foreach(Journal journal in Journals.GetJournals())
        {
            GameObject gridItem = Instantiate(gridItemPrefab, gridTransform);
            Button button = gridItem.GetComponent<Button>();
            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => {
                uiPauseJournal.Setup(journal.text);
            });
        }
    }
    
}

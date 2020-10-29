using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseJournal : UiGenericMenu
{
    
    [SerializeField] private Text journalText;

    public override void Setup()
    {
        
    }

    public void Setup(string text)
    {
        this.journalText.text = text;
    }

}

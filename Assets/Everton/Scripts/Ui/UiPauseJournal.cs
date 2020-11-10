using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiPauseJournal : UiGenericMenu
{
    
    [SerializeField] private Text journalText;
    [SerializeField] private Image backgrondDiary;
    [SerializeField] private Image picture;

    public override void Setup()
    {
        
    }

    public void Setup(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            backgrondDiary.gameObject.SetActive(false);
            picture.gameObject.SetActive(true);
        }
        else
        {
            backgrondDiary.gameObject.SetActive(true);
            picture.gameObject.SetActive(false);
            this.journalText.text = text;
        }
    }

}

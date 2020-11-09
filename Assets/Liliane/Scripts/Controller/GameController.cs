﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    private bool isPaused = false;

    [SerializeField] private UiPauseController uiPauseController;
    [SerializeField] private RasgaController[] rasgas;
    [SerializeField] private PorcoVela[] porcos;
    [SerializeField] private Lamp[] lamps;
    [SerializeField] private List<ObjectToShoot> vasesToDropKey = null;
    public GameObject gameOverPanel;

    public override void Init()
    {
        base.Init();

        if (vasesToDropKey != null && vasesToDropKey.Count > 0)
        {
            int indexSorted = Random.Range(0, vasesToDropKey.Count);
            ObjectToShoot objectToShoot = vasesToDropKey[indexSorted];
            objectToShoot.HasKey();
        }
    }

    public void ActiveGameOver()
    {   
        StartCoroutine("SceneToLoadIE");
    }
    public void ToFinal()
    {
        SceneController.ToFinal();
    }

    private IEnumerator SceneToLoadIE()
    {
        gameOverPanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        uiPauseController.OpenGameOver();
        yield return new WaitForSeconds(1.5f);
        gameOverPanel.SetActive(false);
        
    }

    public void SetPause(bool pause) 
    {
        if (isPaused != pause)
        {
            isPaused = pause;
            foreach (RasgaController rasga in rasgas) rasga?.SetPause(isPaused);
            foreach (PorcoVela porco in porcos) porco?.SetPause(isPaused);
            foreach (Lamp lamp in lamps) lamp?.SetPause(isPaused);
            PlayerController.Instance?.SetPause(isPaused);
        }
    }
    public bool IsPause() => isPaused;

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}

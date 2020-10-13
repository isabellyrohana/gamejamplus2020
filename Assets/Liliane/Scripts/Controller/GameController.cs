using System.Collections;
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
    public GameObject gameOverPanel;

    protected new virtual void Awake()
    {
        base.Awake();
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
        uiPauseController.OpenGameOver();
        yield return new WaitForSeconds(6f);
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
}

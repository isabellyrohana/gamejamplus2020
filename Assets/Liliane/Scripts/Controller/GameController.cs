using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    private bool isPaused = false;

    [SerializeField] private UiPauseController uiPauseController;
    [SerializeField] private RasgaController[] rasgas;
    [SerializeField] private Lamp[] lamps;

    // Start is called before the first frame update
    protected new virtual void Awake()
    {
        base.Awake();
    }
        
    public GameObject gameOverPanel;

    public void ActiveGameOver(string nameSceneToLoad)
    {   
        StartCoroutine("SceneToLoadIE", nameSceneToLoad);
    }
    public void SceneToLoad(string nameSceneToLoad)
    {
        SceneManager.LoadSceneAsync(nameSceneToLoad);
    }

    private IEnumerator SceneToLoadIE(string nameSceneToLoad)
    {
        gameOverPanel.SetActive(true);
        uiPauseController.OpenGameOver();
        yield return new WaitForSeconds(6f);
        gameOverPanel.SetActive(false);
    }

    public void SetPause(bool pause) 
    {
        isPaused = pause;
        foreach (RasgaController rasga in rasgas) rasga?.SetPause(isPaused);
        foreach (Lamp lamp in lamps) lamp?.SetPause(isPaused);
        PlayerController.Instance?.SetPause(isPaused);
    }
    public bool IsPause() => isPaused;
}

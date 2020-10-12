using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{

    private bool isPaused = false;

    [SerializeField] private RasgaController[] rasgas;
    [SerializeField] private Lamp[] lamps;

    // Start is called before the first frame update
    protected new virtual void Awake()
    {
        base.Awake();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPause(bool pause) 
    {
        isPaused = pause;
        foreach (RasgaController rasga in rasgas) rasga?.SetPause(isPaused);
        foreach (Lamp lamp in lamps) lamp?.SetPause(isPaused);
    }
    public bool IsPause() => isPaused;
}

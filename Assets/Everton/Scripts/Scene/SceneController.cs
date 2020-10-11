﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public static void ToMainMenu() => SceneManager.LoadScene(Scenes.MAIN_MENU);

    public static void ToGame() => SceneManager.LoadScene(Scenes.GAME);

    public static void Exit() => Application.Quit();

}

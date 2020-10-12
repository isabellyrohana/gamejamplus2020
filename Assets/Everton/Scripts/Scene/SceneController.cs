using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public static void ToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MAIN_MENU);
        MainMusicController.Instance.PlayThemeAudioClip();
    }

    public static void ToStartScreen() => SceneManager.LoadScene(Scenes.START);
    public static void ToPreGameScreen() => SceneManager.LoadScene(Scenes.PRE_GAME);
    public static void ToReloadScreen() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public static void ToGame() => SceneManager.LoadScene(Scenes.STAGE_02);

    public static void Exit() => Application.Quit();

}

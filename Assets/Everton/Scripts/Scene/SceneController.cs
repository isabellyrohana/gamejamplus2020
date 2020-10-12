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
    public static void ToStage01() => SceneManager.LoadScene(Scenes.STAGE_01);
    public static void ToStage02() => SceneManager.LoadScene(Scenes.STAGE_02);
    public static void ToFinal() => SceneManager.LoadScene(Scenes.FINAL);
    public static void Exit() => Application.Quit();

}

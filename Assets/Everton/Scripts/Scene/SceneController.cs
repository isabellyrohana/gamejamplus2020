using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public static void ToMainMenu()
    {
        SceneManager.LoadScene(Scenes.GetScene(Scenes.ScenesEnum.MAIN_MENU));
        MainMusicController.Instance.PlayThemeAudioClip();
    }

    public static void ToStartScreen() => SceneManager.LoadScene(Scenes.GetScene(Scenes.ScenesEnum.START));
    public static void ToPreGameScreen() => SceneManager.LoadScene(Scenes.GetScene(Scenes.ScenesEnum.PRE_GAME));
    public static void ToReloadScreen() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    public static void ToGame() => SceneManager.LoadScene(Scenes.GetScene(Scenes.ScenesEnum.STAGE_00));
    public static void ToFinal() => SceneManager.LoadScene(Scenes.GetScene(Scenes.ScenesEnum.FINAL));

    public static void ToScene(Scenes.ScenesEnum scene) => SceneManager.LoadScene(Scenes.GetScene(scene));

    public static void Exit() => Application.Quit();

}

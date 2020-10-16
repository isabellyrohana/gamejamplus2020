using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenes : MonoBehaviour
{

    public enum ScenesEnum 
    {
        AWAKE = 0, START = 1, MAIN_MENU = 2, PRE_GAME = 3, STAGE_00 = 4, STAGE_01 = 5, STAGE_02 = 6,
        FINAL = 1000,
        STAGE_02_UI = 1001
    }

    public static string GetScene(ScenesEnum scene)
    {
        switch(scene)
        {
            case ScenesEnum.AWAKE: return "SceneAwake";
            case ScenesEnum.START: return "SceneStart";
            case ScenesEnum.MAIN_MENU: return "SceneMainMenu";
            case ScenesEnum.PRE_GAME: return "ScenePreGame";
            case ScenesEnum.STAGE_00: return "SceneStage00";
            case ScenesEnum.STAGE_01: return "SceneStage01";
            case ScenesEnum.STAGE_02: return "SceneStage02";
            case ScenesEnum.FINAL: return "SceneStage02CuteScene";
            case ScenesEnum.STAGE_02_UI: return "SceneStage02_Ui";
            default: return "SceneMainMenu";
        }
    }
    
}

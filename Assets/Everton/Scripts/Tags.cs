using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    
    public enum TagsEnum 
    {
        PLAYER = 0,
        GROUND = 1,
        THROWABLE = 2,
        ENEMY = 3,
        RASGA = 4,
        CHANDELIER = 5,
        DESTROY = 6,
        PAPER = 7,
        DOOR = 8,
        SAFEPLACE = 9,
        WALL = 10,
    }

    public static string GetTag(TagsEnum scene)
    {
        switch(scene)
        {
            case TagsEnum.PLAYER: return "Player";
            case TagsEnum.GROUND: return "Ground";
            case TagsEnum.THROWABLE: return "Throwable";
            case TagsEnum.ENEMY: return "Enemy";
            case TagsEnum.RASGA: return "Rasga";
            case TagsEnum.CHANDELIER: return "Chandelier";
            case TagsEnum.DESTROY: return "Destroy";
            case TagsEnum.PAPER: return "Paper";
            case TagsEnum.DOOR: return "Door";
            case TagsEnum.SAFEPLACE: return "Safeplace";
            case TagsEnum.WALL: return "Wall";
            default: return "SceneMainMenu";
        }
    }

}

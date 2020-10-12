using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverRasga : MonoBehaviour
{
    public void PlaySfx()
    {
        SoundFxController.Instance.playFx(3);
    }
}

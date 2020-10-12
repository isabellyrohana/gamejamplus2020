using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePreGameController : MonoBehaviour
{

    [SerializeField] private UiFadeEffect bgFadeEffect;

    void Awake()
    {
        bgFadeEffect.FadeIn();
    }

    private void Update()
    {
        if (Input.anyKeyDown) bgFadeEffect.FadeOut(() => {
            SceneController.ToGame();
            MainMusicController.Instance.PlayGameAudioClip();
        });
    }

}

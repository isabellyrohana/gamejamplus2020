using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAwakeController : MonoBehaviour
{
    
    [SerializeField] private UiFadeEffect studioFadeEffect;

    void Awake() {
        studioFadeEffect.FadeIn(() => {
            StartCoroutine(CustomWait.Wait(2f, () => {
                studioFadeEffect.FadeOut(() => {
                    SceneController.ToStartScreen();
                });
            }));
        });
    }

}

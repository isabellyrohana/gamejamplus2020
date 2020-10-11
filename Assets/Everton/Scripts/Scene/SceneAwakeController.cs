using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAwakeController : MonoBehaviour
{

    [SerializeField] private float startTime = 0.5f;
    [SerializeField] private float waitTime = 6f;
    
    [SerializeField] private UiFadeEffect studioFadeEffect;

    void Awake() {
        StartCoroutine(CustomWait.Wait(startTime, () => {
            studioFadeEffect.FadeIn(() => {
                StartCoroutine(CustomWait.Wait(waitTime, () => {
                    studioFadeEffect.FadeOut(() => {
                        SceneController.ToStartScreen();
                    });
                }));
            });
        }));
    }

}

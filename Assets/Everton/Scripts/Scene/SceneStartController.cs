using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartController : MonoBehaviour
{
    
    [SerializeField] private float wait = 2f;

    [SerializeField] private UiFadeEffect logoFadeEffect;

    void Awake() {
        logoFadeEffect.FadeIn(() => {
            StartCoroutine(CustomWait.Wait(wait, () => {
                logoFadeEffect.FadeOut(() => {
                    SceneController.ToMainMenu();
                });
            }));
        });
    }

}

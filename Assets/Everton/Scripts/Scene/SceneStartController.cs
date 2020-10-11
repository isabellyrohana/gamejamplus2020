using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneStartController : MonoBehaviour
{
    
    [SerializeField] private UiFadeEffect logoFadeEffect;

    void Awake() {
        logoFadeEffect.FadeIn(() => {
            StartCoroutine(CustomWait.Wait(2f, () => {
                logoFadeEffect.FadeOut(() => {
                    SceneController.ToMainMenu();
                });
            }));
        });
    }

}

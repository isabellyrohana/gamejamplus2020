using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEngine;

public class SceneStartController : MonoBehaviour
{
    
    [SerializeField] private float wait = 2f;

    [SerializeField] private UiFadeEffect logoFadeEffect;

    private IEnumerator Start() {
        Settings.SettingsFile.Load();

        logoFadeEffect.FadeIn(() => {
            StartCoroutine(CustomWait.Wait(wait, () => {
                logoFadeEffect.FadeOut(() => {
                    StartCoroutine(WaitLoadLocalizationFiles());
                });
            }));
        });

        yield return null;

        IEnumerator WaitLoadLocalizationFiles()
        {
            while(!LocalizationManager.Instance.IsReady)
            {
                yield return null;
            }

            SceneController.ToMainMenu();
        }
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public class UiFadeEffect : MonoBehaviour
{

    [SerializeField] private float animationTime = 1f;
    
    private CanvasGroup _canvasGroup;

    private Coroutine _coroutine;

    void Awake() {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }

    public void FadeIn(Action callback = null)
    {
        if (_coroutine == null) 
        {
            gameObject.SetActive(true);
            _coroutine = StartCoroutine(FadeEffect(callback: callback));
        }
    }

    public void FadeOut(Action callback = null)
    {
        if (_coroutine == null) _coroutine = StartCoroutine(FadeEffect(false, callback));
    }

    private IEnumerator FadeEffect(bool fadeIn = true, Action callback = null)
    {
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        float currentTime = 0f;

        while(currentTime < animationTime)
        {
            currentTime += Time.deltaTime;
            float proportionTime = currentTime / animationTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, proportionTime);
            _canvasGroup.alpha = alpha;

            yield return null;
        }

        _canvasGroup.alpha = endAlpha;
        _coroutine = null;
        callback?.Invoke();

        if (!fadeIn) gameObject.SetActive(false);
        yield return null;
    }

}

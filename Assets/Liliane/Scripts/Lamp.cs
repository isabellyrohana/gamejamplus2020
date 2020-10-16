using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public float minRange = 1f, maxRange = 3f;

    private float randomTwinkle;
    public bool hasSound;

    public Light2D lightLamp;

    private bool _gameIsPaused = false;

    private Coroutine _currentCoroutine = null;

    private void Start()
    {
        _currentCoroutine = StartCoroutine("TwinkleLight");
    }

    private IEnumerator TwinkleLight()
    {
        lightLamp.intensity = 2.5f;
        randomTwinkle = Random.Range(minRange, maxRange);

        yield return new WaitForSeconds(randomTwinkle);

        lightLamp.intensity = 0.1f;
        randomTwinkle = Random.Range(0.01f, 0.25f);
        
        PlaySfx();

        yield return new WaitForSeconds(randomTwinkle);
        lightLamp.intensity = 2.5f;
        
        _currentCoroutine = StartCoroutine("TwinkleLight");
    }

    public void SetPause(bool pause)
    {
        if (pause && _currentCoroutine != null) StopCoroutine(_currentCoroutine);
        else if (!pause) _currentCoroutine = StartCoroutine("TwinkleLight");
    }
    
    private void PlaySfx()
    {
        if(hasSound)
        {
            SoundFxController.Instance.playFx(4);
        }
    }
}

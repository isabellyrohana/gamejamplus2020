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

    private Color _startColor = Color.yellow;

    private void Start()
    {
        _currentCoroutine = StartCoroutine("TwinkleLight");
        _startColor = lightLamp.color;
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
            SoundFxController.Instance.playFx(4, transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.UpdatePlayerOnTheLight(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.UpdatePlayerOnTheLight(false);
        }
    }

    public void StopLight()
    {
        lightLamp.intensity = 0f;
        StopCoroutine(_currentCoroutine);
    }

    public void TurnOffSlowly(float timeToStop)
    {
        StopCoroutine(_currentCoroutine);
        lightLamp.intensity = 2.5f;

        StartCoroutine(Wait());

        IEnumerator Wait()
        {
            float currentTime = 0f;
            float startIntensity = 2.5f;
            float finalIntensity = 0.07f;

            while(currentTime < timeToStop)
            {
                currentTime += Time.deltaTime;
                float proportionTime = MathUtility.CurveAsc(currentTime) / timeToStop;
                Color newColor = Color.Lerp(_startColor, Color.red, proportionTime);
                float intensity = Mathf.Lerp(startIntensity, finalIntensity, proportionTime);

                lightLamp.color = newColor;
                lightLamp.intensity = intensity;
                yield return null;
            }

            lightLamp.color = Color.red;
            lightLamp.intensity = finalIntensity;
            yield return null;
        }
    }

}

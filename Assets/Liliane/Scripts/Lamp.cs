using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    private float randomTwinkle;

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
        randomTwinkle = Random.Range(1f, 3f);

        yield return new WaitForSeconds(randomTwinkle);

        lightLamp.intensity = 0.1f;
        randomTwinkle = Random.Range(0.01f, 0.25f);

        yield return new WaitForSeconds(randomTwinkle);
        lightLamp.intensity = 2.5f;
        
        _currentCoroutine = StartCoroutine("TwinkleLight");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.UpdatePlayerVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            PlayerController.Instance.UpdatePlayerVisible(false);
        }
    }

    public void SetPause(bool pause)
    {
        if (pause && _currentCoroutine != null) StopCoroutine(_currentCoroutine);
        else if (!pause) _currentCoroutine = StartCoroutine("TwinkleLight");
    }

}

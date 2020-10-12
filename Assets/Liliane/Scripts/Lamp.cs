using System.Collections;
using UnityEngine.Experimental.Rendering.Universal;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public float minRange = 1f, maxRange = 3f;

    private float randomTwinkle;

    public Light2D lightLamp;

    public float intensity;



    private void Start()
    {
        StartCoroutine("TwinkleLight");  
    }

    private IEnumerator TwinkleLight()
    {
        lightLamp.intensity = 2.5f;
        randomTwinkle = Random.Range(minRange, maxRange);

        yield return new WaitForSeconds(randomTwinkle);

        lightLamp.intensity = 0.1f;
        randomTwinkle = Random.Range(0.01f, 0.25f);

        yield return new WaitForSeconds(randomTwinkle);
        lightLamp.intensity = 2.5f;
        
        StartCoroutine("TwinkleLight"); 

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(PlayerController.Instance != null)
                PlayerController.Instance.UpdatePlayerVisible(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if (PlayerController.Instance != null)
                PlayerController.Instance.UpdatePlayerVisible(false);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine;

public class Chandelier : MonoBehaviour
{
    private Rigidbody2D chandelierRb;
    public Light2D lightLamp;

    private void Start()
    {
        chandelierRb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("throwable"))
        {
            chandelierRb.gravityScale = 2f;

            RasgaController.Instance.UpdateCanAttack(false);
            RasgaController.Instance.RasgaAffected();
            
            SoundFxController.Instance.playFx(3);
            StartCoroutine("LightsFall");
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            chandelierRb.gravityScale = 0f;
            chandelierRb.velocity = Vector2.zero;
        }
    }


    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            chandelierRb.gravityScale = 0f;
            chandelierRb.velocity = Vector2.zero;
        }
    }

    private IEnumerator LightsFall()
    {

        yield return new WaitForSeconds(Random.Range(0.01f, 0.25f));

        lightLamp.intensity = 0.1f;

        yield return new WaitForSeconds(Random.Range(0.01f, 0.25f));
        lightLamp.intensity = 2.5f;

        yield return new WaitForSeconds(0.25f);

        lightLamp.gameObject.SetActive(false);

    }
}

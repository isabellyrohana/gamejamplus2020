using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToShoot : MonoBehaviour
{
    private bool _gameIsPaused = false;
    private Rigidbody2D objectRb;
    private Animator objectAnim;
    private BoxCollider2D objectBC;
    private bool canDestroy;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody2D>();
        objectAnim = GetComponent<Animator>();
        objectBC = GetComponent<BoxCollider2D>();
    }


    public void ChangeToTrigger()
    {
        objectBC.isTrigger = true;
    }

    /*private IEnumerator SpawnNewObject()
    {
        yield return new WaitForSeconds(4f);
        SpawnObject.Instance.SpawnObjectToShoot();
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Chandelier") || other.gameObject.CompareTag("Wall"))
        {
            SoundFxController.Instance.playFx(7);

            objectAnim.SetTrigger("destroy");
            objectRb.velocity = new Vector2(0, objectRb.velocity.x);
            objectRb.gravityScale = 3f;

            canDestroy = true;
        }

        if (other.gameObject.CompareTag("Bound"))
        {
            canDestroy = true;
        }

        if (other.gameObject.CompareTag("Ground"))
        {
            canDestroy = true;
            objectRb.velocity = Vector2.zero;
            objectRb.gravityScale = 0f;
            objectAnim.SetTrigger("destroy");
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ground")  && canDestroy)
        {
            canDestroy = false;
            SoundFxController.Instance.playFx(7);
            objectRb.velocity = Vector2.zero;
            objectRb.gravityScale = 0f;
            objectAnim.SetTrigger("destroy");
        }
    }
}

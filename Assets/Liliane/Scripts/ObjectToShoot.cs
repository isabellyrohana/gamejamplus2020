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

    public void CanDestroy()
    {
        Destroy(this.gameObject, 4.05f);
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
        if(other.gameObject.CompareTag("Chandelier") || other.gameObject.CompareTag("Bound"))
        {
            print("Chandelier");
            objectAnim.SetTrigger("destroy");
            objectRb.velocity = new Vector2(0, objectRb.velocity.x);
            objectRb.gravityScale = 3f;
            canDestroy = true;
           SoundFxController.Instance.playFx(7);
        }

        if(other.gameObject.CompareTag("Ground") && canDestroy)
        {
            objectRb.velocity = Vector2.zero;
            objectRb.gravityScale = 0f;
            objectAnim.SetTrigger("destroy");
        }
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ground")  && canDestroy)
        {
            objectRb.velocity = Vector2.zero;
            objectRb.gravityScale = 0f;
            objectAnim.SetTrigger("destroy");
        }
    }
}

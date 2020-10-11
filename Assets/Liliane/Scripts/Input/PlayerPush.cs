﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPush : MonoBehaviour
{
    [Header("Position Object Held")]
    public GameObject targetObject;
    public LayerMask boxMask;

    public float distance = 0.5f;

    private PlayerController _playerController;
    private GameObject box;
    
    private float delayToShoot = 2f;
    private int direction = 1;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    void Update()
    {

        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right*transform.localScale.x, distance, boxMask);

        if(hit.collider != null && hit.collider.gameObject.tag=="throwable" && Input.GetButtonDown("Pick"))
        {
            box = hit.collider.gameObject;
            box.TryGetComponent(out Rigidbody2D boxRb);
            boxRb.bodyType = RigidbodyType2D.Dynamic;

            StartCoroutine("KeyIsPressed");
        }
        
        if(hit.collider != null && Input.GetButtonDown("Interact") && box!=null)
        {
            StopCoroutine("KeyIsPressed");
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.TryGetComponent(out Rigidbody2D boxRb);
            box.TryGetComponent(out ObjectToShoot boxScript);
            boxScript.CanDestroy();

            if(_playerController.isLookLeft)
            {
                direction = -1;
            }
            else
            {
                direction = 1;
            }

            boxRb.AddForce(new Vector2(20 * direction, 25), ForceMode2D.Impulse);

        }
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y) + Vector2.right * transform.localScale.x * distance);
    }

    private IEnumerator KeyIsPressed()
    {
        yield return new WaitForSecondsRealtime(0f);
        Hold(box);
        StartCoroutine("KeyIsPressed");
    }

    void Hold(GameObject box){

        box.GetComponent<FixedJoint2D>().enabled = true;
        box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();

        if(Input.GetAxisRaw("Horizontal")!=0)
        {
            box.transform.position = new Vector2(targetObject.transform.position.x, targetObject.transform.position.y);
        }
    }
}

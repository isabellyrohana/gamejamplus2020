using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Friend : MonoBehaviour
{
    private Animator friendAnim;
    private Rigidbody2D friendRb;
    public GameObject manpiguard;

    private void Start()
    {
        friendAnim = GetComponent<Animator>();
        friendRb = GetComponent<Rigidbody2D>();
        StartCoroutine("PlayAnim");
    }
    
    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(2f);
        friendAnim.SetBool("walk", true);
        friendRb.velocity = new Vector2(1.5f, 0);
    }

}

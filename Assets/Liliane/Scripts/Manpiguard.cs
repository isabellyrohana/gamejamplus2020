using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manpiguard : MonoBehaviour
{
    private Rigidbody2D manpiRb;

    private void Start()
    {
        manpiRb = GetComponent<Rigidbody2D>();
        
        StartCoroutine("PlayAnim");
    }

    IEnumerator PlayAnim()
    {
        yield return new WaitForSeconds(0.05f);
        manpiRb.velocity = new Vector2(1.5f, 0);
    }
    
}

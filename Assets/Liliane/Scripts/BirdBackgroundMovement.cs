using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBackgroundMovement : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Destroy"))
        {
            print("desto");
            Destroy(this.gameObject);
        }
    }

}

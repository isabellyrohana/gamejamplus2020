using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour, ThrowAble
{
    [SerializeField]
    private float Throwforce = 10f;

    public void Carry(GameObject player)
    {
        
    }

    public void Throw(Vector3 direction, Vector3 position)
    {
        transform.position = position;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().AddForce(direction * Throwforce);
        transform.parent = null;
        //GetComponent<ParticleSystem>().Play();
    }

}

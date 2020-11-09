using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierBoxCollider : MonoBehaviour
{

    [SerializeField] private SpriteRenderer chandelierSprite = null;
    [SerializeField] private Sprite spriteEndFall = null;

    [SerializeField] private Rigidbody2D rb = null;

    [SerializeField] private Lamp[] lampsToRotate = null;
    
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.collider.CompareTag(Tags.GetTag(Tags.TagsEnum.GROUND)))
        {
            rb.gravityScale = 0f;
            rb.bodyType = RigidbodyType2D.Static;
            chandelierSprite.sprite = spriteEndFall;
            foreach(Lamp lamp in lampsToRotate) lamp.transform.rotation = Quaternion.Euler(0f, 0f, 45f);
        }

    }

}

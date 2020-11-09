using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChandelierFall : MonoBehaviour
{

    [SerializeField] private SpriteRenderer chandelierSprite = null;
    [SerializeField] private ChandelierBoxCollider chandelierBoxCollider = null;
    [SerializeField] private Sprite spriteStartFall = null;
    [SerializeField] private bool playerExitRight = true;
    [SerializeField] private Rigidbody2D rb = null;
    [SerializeField] private BoxCollider2D triggerCollider = null;

    [SerializeField] private Lamp[] lampsToTurnOff = null;
    [SerializeField] private Lamp[] lampsToTurnOffSlowly = null;

    private bool isToFall = false;
    private bool isFalling = false;

    private void Update()
    {
        if (isToFall && !isFalling)
        {
            isFalling = true;
            rb.gravityScale = 3f;
            triggerCollider.enabled = false;
            chandelierBoxCollider.gameObject.SetActive(true);
            chandelierSprite.sprite = spriteStartFall;
            foreach (Lamp lamp in lampsToTurnOff) lamp.StopLight();
            foreach (Lamp lamp in lampsToTurnOffSlowly) lamp.TurnOffSlowly(1f);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            Debug.Log("Player is not trigger anymore with Chandelier");
            bool playerOnRight = other.transform.position.x > transform.position.x;
            isToFall = ((playerOnRight && playerExitRight) || (!playerOnRight && !playerExitRight));
        }

    }

}

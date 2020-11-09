using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToShoot : MonoBehaviour
{

    [SerializeField] private Key keyPrefab;

    private bool _gameIsPaused = false;
    private Rigidbody2D objectRb;
    private Animator objectAnim;
    private BoxCollider2D objectBC;
    private bool canDestroy;

    private bool _hasKey = false;

    private void Start()
    {
        objectRb = GetComponent<Rigidbody2D>();
        objectAnim = GetComponent<Animator>();
        objectBC = GetComponent<BoxCollider2D>();

        canDestroy = true;
    }

    public void ChangeToTrigger()
    {
        objectBC.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.CHANDELIER)) || 
            other.CompareTag(Tags.GetTag(Tags.TagsEnum.WALL)))
        {
            SoundFxController.Instance.playFx(7);

            objectAnim.SetTrigger("destroy");
            objectRb.velocity = new Vector2(0, objectRb.velocity.y);
            objectRb.gravityScale = 3f;

            canDestroy = false;
        }

        if (other.gameObject.CompareTag("Bound"))
        {
            canDestroy = true;
        }

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.GROUND)))
        {
            objectRb.velocity = Vector2.zero;
            objectRb.gravityScale = 0f;
            objectAnim.SetTrigger("destroy");

            SoundFxController.Instance.playFx(7);
            gameObject.tag = "Untagged";

            if (_hasKey) Instantiate(keyPrefab, transform.position + Vector3.up, Quaternion.identity, transform);

            canDestroy = false;
        }

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && canDestroy)
        {
            string key = "K";
            Vector2 position = transform.position + Vector3.up * transform.localScale.y * 7f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);
        }
    }

    public void HasKey()
    {
        _hasKey = true;
    }

}

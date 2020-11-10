using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(SpriteRenderer)),
    RequireComponent(typeof(BoxCollider2D))
]
public class Picture : MonoBehaviour
{
    
    [SerializeField] private Sprite highlightPicture = null;

    private SpriteRenderer spriteRenderer = null;

    private Sprite defaultPicture = null;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultPicture = spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && !PlayerController.Instance.IsInteracting())
        {
            spriteRenderer.sprite = highlightPicture;

            string key = "J";
            Vector2 position = transform.position + Vector3.up * 2.5f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }

    }

    private void OnTriggerStay2D(Collider2D other) {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && !PlayerController.Instance.IsInteracting())
        {
            spriteRenderer.sprite = highlightPicture;

            string key = "J";
            Vector2 position = transform.position + Vector3.up * 2.5f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            spriteRenderer.sprite = defaultPicture;

            Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);
        }

    }

}

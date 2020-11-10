using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JournalCollectable : MonoBehaviour
{

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material papelGlowMaterial;

    private SpriteRenderer _spriteRenderer;

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = defaultMaterial;
    }

    public Journal GetJournal()
    {
        return new Journal("");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && !PlayerController.Instance.IsInteracting())
        {
            _spriteRenderer.material = papelGlowMaterial;

            string key = "J";
            Vector2 position = transform.position + Vector3.up * 5f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && !PlayerController.Instance.IsInteracting())
        {
            _spriteRenderer.material = papelGlowMaterial;

            string key = "J";
            Vector2 position = transform.position + Vector3.up * 5f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            _spriteRenderer.material = defaultMaterial;
            Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);
        }

    }

}

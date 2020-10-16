using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class JournalCollectable : MonoBehaviour
{

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material papelGlowMaterial;

    private SpriteRenderer _spriteRenderer;

    void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material = defaultMaterial;
    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER))) _spriteRenderer.material = papelGlowMaterial;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER))) _spriteRenderer.material = defaultMaterial;
    }

    public Journal GetJournal()
    {
        return new Journal("");
    }

}

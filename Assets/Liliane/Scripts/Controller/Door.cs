using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[
    RequireComponent(typeof(AudioSource)),
    RequireComponent(typeof(Animator))
]
public class Door : MonoBehaviour
{

    [SerializeField] private Scenes.ScenesEnum sceneTransition = Scenes.ScenesEnum.MAIN_MENU;

    private Animator _doorAnim;
    private AudioSource _audioSource;

    private void Awake()
    {
        _doorAnim = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
    }

    public void OpenDoor() => StartCoroutine(ChangeLevel());

    private IEnumerator ChangeLevel()
    {
        _doorAnim.SetTrigger("open");
        _audioSource.Play();
        PlayerController.Instance?.Pause(true);

        while(_audioSource.isPlaying)
        {
            yield return null;
        }

        SceneController.ToScene(sceneTransition);
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            string key = "W";
            Vector2 position = transform.position + Vector3.up * 4.25f;
            Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        }

    }

    private void OnTriggerExit2D(Collider2D other) {
        
        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);
        }

    }

}

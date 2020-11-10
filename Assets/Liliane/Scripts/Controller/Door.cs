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
    [SerializeField] private bool needKey = false;

    private Animator _doorAnim;
    private AudioSource _audioSource;

    private bool _keyIsShow = false;

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

        while (_audioSource.isPlaying)
            yield return null;

        SceneController.ToScene(sceneTransition);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && !_keyIsShow)
            CheckToShowKey();

    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)))
        {
            if (!_keyIsShow)
                CheckToShowKey();
        }

    }

    private void CheckToShowKey()
    {
        if (!needKey) ShowKey();
        else if (PlayerController.Instance.GetHasKey()) ShowKey();
    }

    private void ShowKey()
    {
        string key = "W";
        Vector2 position = transform.position + Vector3.up * 4.25f;
        Events.ObserverManager.Notify<string, Vector2>(NotifyEvent.Interactions.Arrows.Show, key, position);
        _keyIsShow = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag(Tags.GetTag(Tags.TagsEnum.PLAYER)) && _keyIsShow)
            Events.ObserverManager.Notify(NotifyEvent.Interactions.Arrows.Hide);

    }

    public bool NeedKey() => needKey;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicController : Singleton<MainMusicController>
{

    [SerializeField] private AudioClip gameAudioClip;
    [SerializeField] private AudioClip themeAudioClip;

    private AudioSource _audioSource;

    protected new virtual void Awake() 
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameAudioClip()
    {
        _audioSource.Stop();
        _audioSource.clip = gameAudioClip;
        _audioSource.Play();
    }

    public void PlayThemeAudioClip()
    {
        if (_audioSource.clip != null && _audioSource.clip != themeAudioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = themeAudioClip;
            _audioSource.Play();
        }
    }

}

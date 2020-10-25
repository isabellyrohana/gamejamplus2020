using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMusicManager : Singleton<MainMusicManager>
{

    [SerializeField] private AudioClip gameAudioClip;
    [SerializeField] private AudioClip themeAudioClip;

    private AudioSource _audioSource;

    public override void Init() 
    {
        base.Init();

        DontDestroyOnLoad(gameObject);
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayGameAudioClip()
    {
        if (_audioSource != null && _audioSource.clip != gameAudioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = gameAudioClip;
            _audioSource.Play();
        }
    }

    public void PlayThemeAudioClip()
    {
        if (_audioSource != null && _audioSource.clip != themeAudioClip)
        {
            _audioSource.Stop();
            _audioSource.clip = themeAudioClip;
            _audioSource.Play();
        }
    }

    public void StopMusic()
    {
        _audioSource?.Stop();
    }

    public void PauseMusic()
    {
        _audioSource?.Pause();
    }

    public void UnPauseMusic()
    {
        _audioSource?.UnPause();
    }

    public void RestartMusic()
    {
        _audioSource?.Stop();
        _audioSource?.Play();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxController : Singleton<SoundFxController>
{
    public AudioSource audioSourceFx;
    public AudioClip[] soundFx;
    [SerializeField][Range(.1f, 1f)] float vfxVolume = .5f;

    public override void Init()
    {
        base.Init();
        audioSourceFx.loop = false;
        audioSourceFx.volume = vfxVolume;
    }

    public void playFx(int idFx)
    {
        audioSourceFx.volume = vfxVolume;

        if (soundFx[idFx] != null)
        {
            audioSourceFx.PlayOneShot(soundFx[idFx]);
        }    
    }


    public void playFx(int idFx, Vector2 sfxPosition)
    {
        audioSourceFx.volume = vfxVolume;

        if (soundFx[idFx] != null)
        {
            AudioSource.PlayClipAtPoint(soundFx[idFx], sfxPosition);
            
        }
    }

    public bool IsPlaying()
    {
        return audioSourceFx.isPlaying;
    }
}

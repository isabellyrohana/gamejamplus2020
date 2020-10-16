using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFxController : Singleton<SoundFxController>
{
    public AudioSource audioSourceFx;
    public AudioClip[] soundFx;

    public override void Init()
    {
        base.Init();
        audioSourceFx.loop = false;
        audioSourceFx.volume = 0.5f;
    }

    public void playFx(int idFx)
    {
        if(soundFx[idFx] != null)
        {
            audioSourceFx.PlayOneShot(soundFx[idFx]);
        }    
    }

    public bool IsPlaying()
    {
        return audioSourceFx.isPlaying;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private float masterVolumePercent = 1f;
    private float musicVolumePercent = 1f;
    private float sfxVolumePercent = 1f;

    public void Play(AudioClip clip, Vector3 pos)
    {
        AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
    }
}

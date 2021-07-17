using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager _I = null;

    public enum AudioChannel {
        Master,
        SFX,
        Music
    }

    private AudioSource[] musicSources = null;
    private AudioSource SFX2DSource = null;
    private SoundLibrary library;

    private float masterVolumePercent = 1f;
    private float musicVolumePercent = .2f;
    private float sfxVolumePercent = 1f;

    private int activeMusicSource = 0;

    private void Awake()
    {
        if (_I != null) {
            Destroy(gameObject);
        }
        else {
            _I = this;
        
            DontDestroyOnLoad(gameObject);

            library = GetComponent<SoundLibrary>();
            musicSources = new AudioSource[2];

            for (int i = 0; i < 2; i++) {
                GameObject newMusicSource = new GameObject($"Music Source {i + 1}");

                musicSources[i] = newMusicSource.AddComponent<AudioSource>();
                newMusicSource.transform.parent = transform;
            }

            GameObject newSFX2DSource = new GameObject("2D Sound Effect Source");

            SFX2DSource = newSFX2DSource.AddComponent<AudioSource>();
            newSFX2DSource.transform.parent = transform;

            masterVolumePercent = PlayerPrefs.GetFloat("master vol", masterVolumePercent);
            sfxVolumePercent = PlayerPrefs.GetFloat("sfx vol", sfxVolumePercent);
            musicVolumePercent = PlayerPrefs.GetFloat("music vol", musicVolumePercent);
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel) {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
            break;

            case AudioChannel.SFX:
                sfxVolumePercent = volumePercent;
            break;

            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
            break;
        }

        musicSources[0].volume = musicVolumePercent * masterVolumePercent;
        musicSources[1].volume = musicVolumePercent * masterVolumePercent;

        PlayerPrefs.SetFloat("master vol", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx vol", sfxVolumePercent);
        PlayerPrefs.SetFloat("music vol", musicVolumePercent);
    }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1f) {
        activeMusicSource = 1 - activeMusicSource;

        musicSources[activeMusicSource].clip = clip;

        musicSources[activeMusicSource].Play();
        StartCoroutine(MusicCrossfade(fadeDuration));
    }

    public void PlaySound(AudioClip clip, Vector3 pos)
    {
        if (clip != null) {
            AudioSource.PlayClipAtPoint(clip, pos, sfxVolumePercent * masterVolumePercent);
        }
        else {
            Debug.LogError($"Can't Play \"{ clip.name }\" Audio Clip");
        }
    }
    
    public void PlaySound(string clip, Vector3 pos)
    {
        PlaySound(library.GetClipFromName(clip), pos);
    }

    public void PlaySound2D(string soundName)
    {
        SFX2DSource.PlayOneShot(library.GetClipFromName(soundName), sfxVolumePercent * masterVolumePercent);
    }

    private IEnumerator MusicCrossfade(float duration)
    {
        float percent = 0f;

        while (percent < 1) {
            percent += Time.deltaTime * 1 / duration;

            musicSources[activeMusicSource].volume = Mathf.Lerp(0f, musicVolumePercent * masterVolumePercent, percent);
            musicSources[1 - activeMusicSource].volume = Mathf.Lerp(musicVolumePercent * masterVolumePercent, 0f, percent);

            yield return null;
        }
    }
}

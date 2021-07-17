using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip mainTheme = null;
    [SerializeField] private AudioClip menuTheme = null;

    private void Start()
    {
        AudioManager._I.PlayMusic(menuTheme, 2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            AudioManager._I.PlayMusic(mainTheme, 3f);
        }
    }
}

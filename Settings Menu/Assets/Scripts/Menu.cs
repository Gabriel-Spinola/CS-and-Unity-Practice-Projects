using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] private Slider[] volumeSliders;
    [SerializeField] private Toggle[] resolutionToggles;

    [SerializeField] private int[] screenWidths;

    private int activeScreenResIndex;

    public void Play() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    public void Quit() => Application.Quit();

    public void SetScreenResolution(int index)
    {
        if (resolutionToggles[index].isOn) {
            float aspectRatio = 16f / 9f;

            activeScreenResIndex = index;

            Screen.SetResolution(screenWidths[index], (int) (screenWidths[index] / aspectRatio), false);
        }
    }

    public void SetFullscreen(bool isFullscreen)
    {
        for (int i = 0; i < resolutionToggles.Length; i++) {
            resolutionToggles[i].interactable = !isFullscreen;
        }

        if (isFullscreen) {
            Resolution[] allResolutions = Screen.resolutions;
            Resolution maxResolution = allResolutions[allResolutions.Length - 1];

            Screen.SetResolution(maxResolution.width, maxResolution.height, true);
        }
        else {
            SetScreenResolution(activeScreenResIndex);
        }
    }

    public void SetMasterVolume(float volume)
    {

    }

    public void SetMusicVolume(float volume)
    {

    }
    
    public void SetSFXVolume(float volume)
    {

    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private SceneTheme[] sceneThemes;

	private string sceneName;

    private void Start()
    {
		OnLevelWasLoaded(0);
    }

	void OnLevelWasLoaded(int sceneIndex)
	{
		string newSceneName = SceneManager.GetActiveScene().name;

		if (newSceneName != sceneName) {
			sceneName = newSceneName;

			Invoke(nameof(PlayMusic), .2f);
		}
	}

	void PlayMusic()
	{
		AudioClip clipToPlay = null;
		float fadeDuration = 0f;

        for (int i = 0; i < sceneThemes.Length; i++) {
			if (sceneName == sceneThemes[i].name) {
				clipToPlay = sceneThemes[i].theme;
				fadeDuration = sceneThemes[i].fadeDuration;
            }
        }

		if (clipToPlay != null) {
			AudioManager._I.PlayMusic(clipToPlay, fadeDuration);

			Invoke(nameof(PlayMusic), clipToPlay.length);
		}
	}

	[System.Serializable]
	public class SceneTheme
    {
		public string name;
		public AudioClip theme;
		public float fadeDuration;
    }
}

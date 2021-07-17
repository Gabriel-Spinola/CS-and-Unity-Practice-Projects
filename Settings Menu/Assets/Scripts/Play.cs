using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{
    public void PlayShootSound()
    {
        AudioManager._I.PlaySound2D("Shoot Sound", 1.5f);
    }
    
    public void PlayJumpSound()
    {
        AudioManager._I.PlaySound2D("Jump Sound");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
    
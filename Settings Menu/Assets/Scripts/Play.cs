using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
    
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDelegateTest : MonoBehaviour
{
    [SerializeField] private ActionOnTimer actionOnTimer;

    void Start()
    {
        actionOnTimer.SetTimer(1f, () => {
            Debug.Log("Timer is Complete");
        });
    }
}

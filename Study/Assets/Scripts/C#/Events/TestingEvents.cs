using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TestingEvents : MonoBehaviour
{
    /**
     * It's pretty likely how the observer pattern works
     * With a Publisher that have the event and the subscribers that can subscribe to that event.
    */

    // A Event With EventHandler delegate
    public event EventHandler OnSpacePressed;

    // A Event With EventHandler delegate and a class as parameter
    public event EventHandler<OnEPressedEventArgs> OnEPressed;

    private int keyDownCount;

    // Event parameter class that have the public int keyDownCount Variable
    public class OnEPressedEventArgs : EventArgs {
        public int keyDownCount;
        
        public void argFunction()
        {
            Debug.Log("Arg func");
        }
    }

    // Own delegate Event
    public delegate void TestEventDelegate(float f);
    public event TestEventDelegate OnFloatEvent;

    // Event with Action delegate
    public event Action<bool, int> OnActionEvent;

    // Events with Unity delage (shows in inspector, but it's less optimal)
    public UnityEvent OnUnityEvent;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            OnSpacePressed?.Invoke(this, EventArgs.Empty); // trigger the event if it's not null
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            keyDownCount++;

            // Trigger the event, pass the class as paremeter and set the keyDownCount variable
            OnEPressed?.Invoke(this, new OnEPressedEventArgs { 
                keyDownCount = this.keyDownCount
            });
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            // trigger the events if it's not null
            OnFloatEvent?.Invoke(10.2f);
            OnActionEvent?.Invoke(true, 10);
            OnUnityEvent?.Invoke();
        }
    }
}

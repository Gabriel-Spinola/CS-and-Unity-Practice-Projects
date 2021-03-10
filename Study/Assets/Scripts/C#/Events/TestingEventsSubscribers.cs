using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TestingEventsSubscribers : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TestingEvents testingEvents = GetComponent<TestingEvents>();

        // Add a new Subscribers
        testingEvents.OnSpacePressed += TestingEvents_OnSpacePressed;
        testingEvents.OnEPressed += TestingEvents_OnEPressed;
        testingEvents.OnFloatEvent += TestingEvents_OnFloatEvent;
        testingEvents.OnActionEvent += TestingEvents_OnActionEvent;
    }

    private void TestingEvents_OnActionEvent(bool arg1, int arg2)
    {
        Debug.Log($"Bool: { arg1 }, Int: { arg2 } ");
    }

    private void TestingEvents_OnFloatEvent(float f)
    {
        Debug.Log($"Float:+ { f }");
    }

    // Subscriber Function
    private void TestingEvents_OnEPressed(object sender, TestingEvents.OnEPressedEventArgs e)
    {
        // Getting the parameter class attributes and methods by e
        Debug.Log($"E Pressed: { e.keyDownCount} times ");

        e.argFunction();
    }

    // Subscriber Function
    private void TestingEvents_OnSpacePressed(object sender, EventArgs e)
    {
        Debug.Log("Space 2");

        TestingEvents testingEvents = GetComponent<TestingEvents>();

        // Unsubscribe
        testingEvents.OnSpacePressed -= TestingEvents_OnSpacePressed;
    }

    public void TestingUnityEvent()
    {
        Debug.Log("Unity Event triggered");
    }
}

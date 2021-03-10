using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingDelegates : MonoBehaviour
{
    public delegate void TestDelegate();
    public delegate bool TestBoolDelegate(int i);

    private TestDelegate testDelegateFunction;
    private TestBoolDelegate testBoolDelegateFunction;

    private Action testAction;
    private Action<int, float> testIntFloatAction;

    // A func needs to return some type
    private Func<bool> testBoolFunc;
    
    // Int as parameter and bool as return type
    private Func<int, bool> testIntBoolFunc;

    void Start()
    {
        /**
         * ``` C#
         *  // Assing the delegate to a function and use it
         *  testDelegateFunction = MyTestDelegateFunction;
         *
         *  testDelegateFunction();
         *
         *  // Re-assing the delegate to another function and use it
         *  testDelegateFunction = MySecondTestDelegateFunction;
         *
         *  testDelegateFunction();
         * ```
        */

        /**
         * ``` C#
         * // Assigning multiple functions in one delegate
         * testDelegateFunction += MyTestDelegateFunction;
         * testDelegateFunction += MySecondTestDelegateFunction;
         * 
         * testDelegateFunction();
         * 
         * // Removing a function from a delegate
         * testDelegateFunction -= MySecondTestDelegateFunction;
         * 
         * testDelegateFunction();
         * ```
        */

        /// Assigning delegates with function
        testBoolDelegateFunction = MyTestBoolDelegateFunction;

        Debug.Log(testBoolDelegateFunction(2));

        /// Assigning delegates with anonymous function (it is not possible to remove the function later)
        testDelegateFunction = delegate () {
            Debug.Log("Anonymous method");
        };

        testDelegateFunction();

        /// Assigning delegates with lambda expression (it is not possible to remove the function later)
        testDelegateFunction = () => {
            Debug.Log("lambda Expression");
        };

        testDelegateFunction();

        testBoolDelegateFunction = (int i) => {
            return i > 0;
        };

        Debug.Log(testBoolDelegateFunction(-1));

        /// Action Delegate
        testIntFloatAction = (int i, float f) => {
            Debug.Log("test int float Action!");
        };

        /// Func Delegates
        testBoolFunc = () => true;
        testIntBoolFunc = (int i) => i < 6;

        Debug.Log(testBoolFunc());
        Debug.Log(testIntBoolFunc(8));
    }

    private void MyTestDelegateFunction()
    {
        Debug.Log("MyTestDelegateFunction");
    }

    private void MySecondTestDelegateFunction()
    {
        Debug.Log("MySecondtestDelegateFunction");
    }

    private bool MyTestBoolDelegateFunction(int i)
    {
        return 1 > 0;
    }

    void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGenerics : MonoBehaviour
{
    private delegate void MyActionDelegate<T1, T2>(T1 t1, T2 t2);
    private Action<int, string> action;

    private delegate TResult MyFuncDelegate<T1, TResult>(T1 t1);
    private Func<int, bool> func;

    void Start()
    {
        // You don't need to write the type definition in the front of the function name
        int[] intArray = CreateArray<int>(5, 6);
        string[] stringArray = CreateArray<string>("Hello", "World");

        Debug.Log($"Int Array: Length: { intArray.Length }, Index 0: { intArray[0] }, Index 1: { intArray[1] }");
        Debug.Log($"Int Array: Length: { stringArray.Length }, Index 0: { stringArray[0] }, Index 1: { stringArray[1] }");

        TestMultiGenerics<int, string>(1, "Hello");

        // Instiate the class
        MyClass<EnemyMinion> myClass = new MyClass<EnemyMinion>(new EnemyMinion());
        MyClass<EnemyArcher> myClass2 = new MyClass<EnemyArcher>(new EnemyArcher());
    }

    /// <summary>
    /// The Generic Signature makes the type of the fuction something like auto in c++,
    /// or a normal variable in javascript
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="firstElement"></param>
    /// <param name="secondElement"></param>
    /// <returns></returns>
    private T[] CreateArray<T>(T firstElement, T secondElement)
    {
        return new T[] {
            firstElement, secondElement
        };
    }

    private void TestMultiGenerics<T1, T2>(T1 t1, T2 t2)
    {
        Debug.Log(t1.GetType());
        Debug.Log(t2.GetType());
    }
}

// Adding constraints to the T generic
// Just use T if its implements the IEnemy interface
public class MyClass<T> where T : IEnemy
{
    public T value;

    public MyClass(T value)
    {
        value.Demage();
    }

    private T[] CreateArray(T firstElement, T secondElement)
    {
        return new T[] {
            firstElement, secondElement
        };
    }
}

public interface IEnemy
{
    void Demage();
}

public class EnemyMinion : IEnemy
{
    public void Demage()
    {
        Debug.Log("EnemyMinion.Demage()");
    }
}

public class EnemyArcher : IEnemy
{
    public void Demage()
    {
        Debug.Log("EnemyArcher.Demage()");
    }
}

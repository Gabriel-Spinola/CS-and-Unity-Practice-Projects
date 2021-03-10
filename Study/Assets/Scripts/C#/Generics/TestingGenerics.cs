using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGenerics : MonoBehaviour
{
    void Start()
    {
        int[] intArray = CreateArray<int>(5, 6);
        string[] stringArray = CreateArray<string>("Hello", "World");

        Debug.Log($"Int Array: Length: { intArray.Length }, Index 0: { intArray[0] }, Index 1: { intArray[1] }");
        Debug.Log($"Int Array: Length: { stringArray.Length }, Index 0: { stringArray[0] }, Index 1: { stringArray[1] }");
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
}

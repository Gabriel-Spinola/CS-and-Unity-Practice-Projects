using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingGenerics : MonoBehaviour
{
    void Start()
    {
        int[] intArray = CreateArray(5, 6);

        Debug.Log($"{ intArray.Length }  { intArray[0] }  { intArray[1] }");
    }

    private int[] CreateArray(int firstElement, int secondElement)
    {
        return new int[] {
            firstElement, secondElement
        };
     }
}

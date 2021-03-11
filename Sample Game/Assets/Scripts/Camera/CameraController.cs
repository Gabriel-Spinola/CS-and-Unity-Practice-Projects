using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    void Update()
    {
        transform.position = Vector3.right * playerPos.position.x;
    }
}

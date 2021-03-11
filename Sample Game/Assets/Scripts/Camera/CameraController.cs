using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform playerPos;

    void Update()
    {
        // my code
        // transform.position = Vector3.right * playerPos.position.x;

        // tutorial code
        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, playerPos.position.z);
    }
}

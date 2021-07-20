using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 0f;
    [SerializeField] private float friction = 0f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 0f;

    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {

    }

    public void Die()
    {

    }
}

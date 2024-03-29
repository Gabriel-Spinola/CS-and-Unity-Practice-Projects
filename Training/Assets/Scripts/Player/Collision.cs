﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask whatIsBlocks;

    [Header("Horizontal Collision")]
    [SerializeField] private Vector2 rightColOffset;
    [SerializeField] private Vector2 leftColOffset;

    [Range(0f, 1f)]
    [SerializeField] private float horizontalColRadius = .5f;

    [Header("Vertical Collision")]
    [SerializeField] private Vector2 bottomColSize;
    [SerializeField] private Vector2 bottomColOffset;

    [Space]

    public bool isOnRightWall = false;
    public bool isOnLeftWall = false;
    public bool isOnWall = false;

    [Space]

    public bool isGrounded = false;

    private void Update()
    {
        isOnRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightColOffset, horizontalColRadius, whatIsBlocks);
        isOnLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftColOffset, horizontalColRadius, whatIsBlocks);
        isOnWall = isOnLeftWall || isOnRightWall;

        isGrounded = Physics2D.OverlapBox((Vector2) transform.position + bottomColOffset, bottomColSize, 0f, whatIsBlocks);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2) transform.position + rightColOffset, horizontalColRadius);
        Gizmos.DrawWireSphere((Vector2) transform.position + leftColOffset, horizontalColRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((Vector2) transform.position + bottomColOffset, bottomColSize);
    }
}

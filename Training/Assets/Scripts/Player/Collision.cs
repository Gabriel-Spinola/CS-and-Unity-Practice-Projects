using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsBlocks;

    [SerializeField] private Vector2 rightColOffset;
    [SerializeField] private Vector2 leftColOffset;

    [SerializeField] private Vector2 bottomColSize;
    [SerializeField] private Vector2 bottomColOffset;

    [SerializeField] private float horizontalColRadius = .5f;
    [SerializeField] private float bottomColRadius = .5f; 

    public bool isOnRightWall = false;
    public bool isOnLeftWall = false;
    public bool isGrounded = false;

    private void Update()
    {
        isOnRightWall = Physics2D.OverlapCircle((Vector2) transform.position + rightColOffset, horizontalColRadius, whatIsBlocks);
        isOnLeftWall = Physics2D.OverlapCircle((Vector2) transform.position + leftColOffset, horizontalColRadius, whatIsBlocks);

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

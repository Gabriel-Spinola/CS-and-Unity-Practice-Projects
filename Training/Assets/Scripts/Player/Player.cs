using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collision))]
public class Player : MonoBehaviour, ICharacter
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float lerpMoveSpeed = 8f;
    [SerializeField] private float friction = .2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    [Header("Walljump")]
    [SerializeField] private float wallJumpForce = 10f;

    private Rigidbody2D rb = null;
    private Collision collision = null;

    private bool canMove = true;
    private bool wallJumped = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        collision = GetComponent<Collision>();
    }

    void Update()
    {
        BetterJump();

        if (canMove)
            Movement();

        if (collision.isGrounded) {
            wallJumped = false;
        }

        if (InputManager._I.keyJumping) {
            if (collision.isOnWall && !collision.isGrounded) {
                WallJump();
            }

            if (collision.isGrounded) {
                Jump();
            }
        }
    }

    private void Movement()
    {
        if (InputManager._I.xAxis != 0f) {
            rb.velocity = new Vector2(moveSpeed * InputManager._I.xAxis, rb.velocity.y);
        }
        else {
            if (!wallJumped) {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, friction), rb.velocity.y);
            }
            else {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, friction - .12f), rb.velocity.y);
            }
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
    
    private void Jump(Vector2 jumpDir)
    {
        rb.velocity = jumpDir;
    }

    private void WallJump()
    {
        StartCoroutine(DisableMovement(.2f));

        int wallJumpDir = collision.isOnRightWall ? -1 : collision.isOnLeftWall ? 1 : 0;
        wallJumped = true;

        Jump(Vector2.right * wallJumpForce / 1.5f * wallJumpDir + Vector2.up * wallJumpForce / 1.2f);
    }

    /// <summary>
    /// if falling, add fallMultiplier
    /// if jumping and not holding spacebar, increase gravity to peform a small jump
    /// if jumping and holding spacebar, perform a full jump
    /// </summary>
    private void BetterJump()
    {
        if (rb.velocity.y < 0) {
            rb.velocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !InputManager._I.keyJumpingHold || wallJumped) {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private IEnumerator DisableMovement(float time)
    {
        canMove = false;

        yield return new WaitForSeconds(time);

        canMove = true;
    }

    public void TakeDamage(float damage)
    {

    }

    public void Die()
    {

    }
}

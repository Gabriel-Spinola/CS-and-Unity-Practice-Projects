using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour, ICharacter
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float friction = .2f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2;

    [Header("Walljump")]
    [SerializeField] private float wallJumpForce = 10f;

    private Rigidbody2D rb = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        BetterJump();
        Movement();

        if (InputManager._I.keyJumping) {
            Jump();
        }
    }

    private void Movement()
    {
        if (InputManager._I.xAxis != 0f) {
            rb.velocity = new Vector2(moveSpeed * InputManager._I.xAxis, rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0f, friction), rb.velocity.y);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
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
        else if (rb.velocity.y > 0 && !InputManager._I.keyJumpingHold) {
            rb.velocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void TakeDamage(float damage)
    {

    }

    public void Die()
    {

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float counterMovement;

    [SerializeField] private float jumpForce;

    float xAxis;

    bool jumpKey;

    float threshold = 0.03f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        animator.SetBool("isRunning", false);
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetKeyDown(KeyCode.Space) | Input.GetKey(KeyCode.Space);

        FlipSprite();
        Animation();
    }

    private void FixedUpdate()
    {
        Movement();
        CounterMovement();
        Jump();
    }

    private void Movement()
    {
        if (rb.velocity.x >= maxSpeed) xAxis = 0;
        if (rb.velocity.x <=-maxSpeed) xAxis = 0;

        rb.AddForce(xAxis * transform.right * moveSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
    }

    private void CounterMovement()
    {
        if (!CanJump())
            return;

        if (
            ( Mathf.Abs(rb.velocity.x) >= threshold && xAxis == 0 ) ||
            ( Mathf.Abs(rb.velocity.x) <= threshold && xAxis == 0 )
        ) {
            rb.AddForce(transform.right * -rb.velocity.x * Time.fixedDeltaTime * counterMovement, ForceMode2D.Force);
        }
    }

    private void Jump()
    {
        if (jumpKey && CanJump()) {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime * 6f, ForceMode2D.Impulse);

            return;
        }
    }

    private void Animation()
    {
        if (xAxis < 0 || xAxis > 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);
    }

    private void FlipSprite() => transform.localScale = xAxis < 0 ? new Vector2(-1f, 1f) : (xAxis > 0 ? new Vector2(1f, 1f) : new Vector2(transform.localScale.x, 1f));

    private bool CanJump() => Physics2D.Raycast(transform.position, Vector3.down, 1f, whatIsGround);
}

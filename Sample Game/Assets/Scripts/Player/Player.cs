using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
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

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetKeyDown(KeyCode.Space) | Input.GetKey(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        Movement();
        CounterMovement();
        Jump();
    }

    private void Movement()
    {
        if (rb.velocity.magnitude >= maxSpeed)
            return;

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
            rb.AddForce(transform.right * -rb.velocity.x * Time.fixedDeltaTime * counterMovement);
        }
    }

    private void Jump()
    {
        if (jumpKey && CanJump()) {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime * 6f, ForceMode2D.Force);

            return;
        }
    }

    private bool CanJump() => Physics2D.Raycast(transform.position, Vector3.down, 1f, whatIsGround);
}

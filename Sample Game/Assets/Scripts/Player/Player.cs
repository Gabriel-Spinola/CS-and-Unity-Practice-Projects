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

    float threshold = 0.3f;
    bool isOnAir;

    private void Start()
    {
        
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetKeyDown(KeyCode.Space) | Input.GetKey(KeyCode.Space);

        Debug.DrawRay(transform.position, Vector3.down * 1f, Color.blue);
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

        rb.AddForce(xAxis * Vector3.right * moveSpeed * Time.fixedDeltaTime);
    }

    private void CounterMovement()
    {
        //if (isOnAir)
        //    return;

        if (
            ( Mathf.Abs(rb.velocity.x) >= threshold && xAxis == 0 ) ||
            ( Mathf.Abs(rb.velocity.x) <= threshold && xAxis == 0 )
        ) {
            rb.AddForce(rb.velocity.x * rb.position * Time.fixedDeltaTime * counterMovement);
        }
    }

    private void Jump()
    {
        if (jumpKey && CanJump()) {
            rb.AddForce(Vector3.up * jumpForce);

            isOnAir = true;
        }
    }

    private bool CanJump()
    {
        bool isGrounded = Physics2D.Raycast(transform.position, Vector3.down, 1f, whatIsGround);

        Debug.Log("Grounded");

        return isGrounded;
    }
}

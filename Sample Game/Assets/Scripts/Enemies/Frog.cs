using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float leftMaxDistance = 0f;
    [SerializeField] private float rightMaxDistance = 0f;

    [SerializeField] private float jumpLength = 0f;
    [SerializeField] private float jumpHeight = 0f;

    private Rigidbody2D rb;
    private Collider2D col;
    private Animator animator;

    [SerializeField] private bool isFacingLeft = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        col = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(animator.GetBool("isJumping")) {
            if (rb.velocity.y < .1f) {
                animator.SetBool("isFalling", true);
                animator.SetBool("isJumping", false);
            }
        }

        if(col.IsTouchingLayers(whatIsGround) && animator.GetBool("isFalling")) {
            animator.SetBool("isFalling", false);
        }

        // Check if the next jump will make him transpass the limits
        if (transform.position.x - jumpLength <= leftMaxDistance) {
            isFacingLeft = false;
        }

        // Check if the next jump will make him transpass the limits
        if (transform.position.x + jumpLength >= rightMaxDistance) {
            isFacingLeft = true;
        }
    }

    private void Movement()
    {
        if (isFacingLeft) {
            if (transform.position.x > leftMaxDistance) {
                

                if (transform.localScale.x != 1) {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                if (col.IsTouchingLayers(whatIsGround)) {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);

                    animator.SetBool("isJumping", true);
                }
            }
            else {
                isFacingLeft = false;
            }
        }
        else {
            if (transform.position.x < rightMaxDistance) {
                

                if (transform.localScale.x != -1) {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                if (col.IsTouchingLayers(whatIsGround)) {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);

                    animator.SetBool("isJumping", true);
                }
            }
            else {
                isFacingLeft = true;
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;

    [SerializeField] private float leftMaxDistance;
    [SerializeField] private float rightMaxDistance;

    [SerializeField] private float jumpLength;
    [SerializeField] private float jumpHeight;

    private Rigidbody2D rb;
    private Collider2D col;

    [SerializeField] private bool isFacingLeft = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        col = GetComponent<Collider2D>();   
    }

    private void Update()
    {
        Animation();
    }

    private void FixedUpdate()
    {
        Movement();
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
                }

                // Check if the next jump will make him transpass the limits
                if (transform.position.x + ( ( jumpLength + jumpHeight ) / Time.fixedDeltaTime ) <= leftMaxDistance) {
                    isFacingLeft = true;
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
                }

                // Check if the next jump will make him transpass the limits
                if (transform.position.x - ( ( jumpLength + jumpHeight ) / Time.fixedDeltaTime ) >= rightMaxDistance) {
                    isFacingLeft = true;
                }
            }
            else {
                isFacingLeft = true;
            }
        }
    }

    private void Animation()
    {

    }
}

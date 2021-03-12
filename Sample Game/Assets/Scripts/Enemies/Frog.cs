﻿using System.Collections;
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

    [SerializeField] private bool isFacingLeft = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        col = GetComponent<Collider2D>();   
    }

    private void FixedUpdate()
    {
        if (isFacingLeft) {
            if (transform.position.x > leftMaxDistance) {
                // Check if the next jump will make him transpass the limits
                if (transform.position.x - jumpLength <= leftMaxDistance) {
                    isFacingLeft = false;
                }

                if (transform.localScale.x != 1) {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }

                if (col.IsTouchingLayers(whatIsGround)) {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                }
            }
            else {
                isFacingLeft = false;
            }
        }
        else {
            if (transform.position.x < rightMaxDistance) {
                // Check if the next jump will make him transpass the limits
                if (transform.position.x + jumpLength >= rightMaxDistance) {
                    isFacingLeft = true;
                }

                if (transform.localScale.x != -1) {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                if (col.IsTouchingLayers(whatIsGround)) {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                }
            }
            else {
                isFacingLeft = true;
            }
        }
    }

}

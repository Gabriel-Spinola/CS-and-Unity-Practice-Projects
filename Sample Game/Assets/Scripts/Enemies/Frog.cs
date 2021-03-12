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

    private bool isFacingLeft = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        col = GetComponent<Collider2D>();   
    }

    private void Update()
    {
        if (isFacingLeft) {
            if (transform.position.x >= leftMaxDistance) {
                if (col.IsTouchingLayers(whatIsGround)) {
                    //rb.AddForce();
                }
            }
            else {
                isFacingLeft = false;
            }
        }
    }
}

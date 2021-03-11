using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float counterMovement;

    float xAxis;

    float threshold = 0.3f;

    private void Start()
    {
        
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude >= maxSpeed)
            return;

        rb.AddForce(xAxis * Vector3.right * moveSpeed * Time.fixedDeltaTime);

        if(
            (Mathf.Abs(rb.velocity.x) >= threshold  && xAxis == 0 ) || 
            (Mathf.Abs(rb.velocity.x) <= threshold && xAxis == 0)
        ) {
            rb.AddForce(rb.velocity.x * rb.position * Time.fixedDeltaTime * counterMovement);
        }
    }
}

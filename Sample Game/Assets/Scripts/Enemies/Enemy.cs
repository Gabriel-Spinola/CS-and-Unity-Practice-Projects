using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void JumpedOn() 
    {
        Destroy(rb);

        animator.SetTrigger("whenDead");
    } 

    private void Die() {
        Destroy(gameObject);
    }
}

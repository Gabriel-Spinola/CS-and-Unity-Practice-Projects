using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake() => animator = GetComponent<Animator>();

    public void JumpedOn() => animator.SetTrigger("whenDead");

    private void Die() {
        Destroy(gameObject);
    }
}

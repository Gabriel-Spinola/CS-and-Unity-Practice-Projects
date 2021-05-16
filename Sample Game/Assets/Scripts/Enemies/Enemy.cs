using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Animator animator;

    public void JumpedOn() => animator.SetTrigger("whenDead");

    protected virtual void Awake() => animator = GetComponent<Animator>();

    private void Die() {
        Destroy(gameObject);
    }
}

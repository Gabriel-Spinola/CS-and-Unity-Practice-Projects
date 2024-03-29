﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Animator animator;
    [SerializeField] private TMP_Text collectableType;
    [SerializeField] private TMP_Text collectableCounter;
    [SerializeField] private AudioSource footStep;

    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private LayerMask whatIsEnemy;

    [SerializeField] private int health;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float counterMovement;

    [SerializeField] private float jumpForce;

    [SerializeField] private float hitForce;

    private enum EANIM_STATES 
    { 
        IDLE    = 0,
        RUN     = 1,
        JUMP    = 2,
        FALLING = 3,
        HURT    = 4,
    };

    private EANIM_STATES animState = EANIM_STATES.IDLE;
    [SerializeField] private EANIM_STATES simpleState = EANIM_STATES.IDLE;

    private float threshold = 0.03f;
    private float xAxis;

    private int cherriesUI = 0;

    private bool jumpKey;

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        jumpKey = Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space);

        simpleState = rb.velocity.y < 0 ? EANIM_STATES.FALLING : EANIM_STATES.IDLE;

        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene("Game");

        if (animState != EANIM_STATES.HURT) {
            FlipSprite();
        }

        StartCoroutine(Animation());
        UpdateUI();
        Die();
    }

    private void FixedUpdate()
    {
        if (animState != EANIM_STATES.HURT) {
            Movement();
            CounterMovement();
            Jump();
        } 
    }

    private void Movement()
    {
        if (rb.velocity.x >= maxSpeed) xAxis = 0;
        if (rb.velocity.x <=-maxSpeed) xAxis = 0;

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
            rb.AddForce(transform.right * -rb.velocity.x * Time.fixedDeltaTime * counterMovement, ForceMode2D.Force);
        }
    }

    private void Jump(Action Callback = null)
    {
        if (jumpKey && CanJump()) {
            rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime * 6f, ForceMode2D.Impulse);
            animState = EANIM_STATES.JUMP;

            return;
        }

        Callback?.Invoke();
    }

    private IEnumerator Animation()
    {
        animator.SetInteger("state", (int) animState);

        if(animState == EANIM_STATES.JUMP && CanJump() && animState != EANIM_STATES.HURT) {
            animState = EANIM_STATES.IDLE;
        }

        if (animState == EANIM_STATES.JUMP){
            yield return new WaitForSeconds(.25f);
        }
        else if (xAxis > 0 || xAxis < 0) {
            animState = EANIM_STATES.RUN;
        }
        else if (animState == EANIM_STATES.HURT) {
            yield return new WaitForSeconds(.5f);

            if (rb.velocity.x >= Mathf.Epsilon) {
                animState = EANIM_STATES.IDLE;
            }
        }
        else {
            animState = EANIM_STATES.IDLE;
        }
    }

    private void UpdateUI()
    {
        collectableType.SetText("Cherries: ");
        collectableCounter.SetText(cherriesUI.ToString());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Collectable-Cherry") {
            cherriesUI++;

            Heal(1);
            Destroy(col.gameObject);

            return;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemies-Frog")) {
            Enemy enemy_ = col.gameObject.GetComponent<Enemy>();

            if (Physics2D.Raycast(transform.position, Vector3.down, 1.2f, whatIsEnemy)) {
                enemy_.JumpedOn();

                Jump(() => {
                    rb.AddForce(Vector3.up * jumpForce * Time.fixedDeltaTime * 2.5f, ForceMode2D.Impulse);
                });
            }
            else {
                animState = EANIM_STATES.HURT;

                // Enemy is to my right therefore i should be demaged and move left
                if (col.gameObject.transform.position.x > transform.position.x) {
                    health--;

                    rb.AddForce(new Vector2(-hitForce, hitForce), ForceMode2D.Impulse);
                }
                // Enemy is to my left therefore i should be demaged and move right
                else {
                    health--;

                    rb.AddForce(new Vector2(hitForce, hitForce), ForceMode2D.Impulse);
                }
            }
        }

        if (col.gameObject.layer == 9) {
            SceneManager.LoadScene("Game");
        }
    }

    private void FootstepSound()
    {
        if (CanJump()) {
            footStep.pitch = UnityEngine.Random.Range(1.5f, 3f);

            footStep.Play();
        }
    }

    private void FlipSprite() => transform.localScale = xAxis < 0 ? new Vector2(-1f, 1f) : ( xAxis > 0 ? new Vector2(1f, 1f) : new Vector2(transform.localScale.x, 1f) );

    private bool CanJump() => Physics2D.Raycast(transform.position, Vector3.down, .98f, whatIsGround);

    private void Die() { if (health <= 0) SceneManager.LoadScene("Game"); }

    private void Heal(int lifePoints) => health += health < 3 ? lifePoints : 0;
}
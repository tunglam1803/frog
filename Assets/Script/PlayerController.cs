using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    private int speed = 4;
    private float h_move;
    private float jumpForce = 7f;
    private float fallMultiplier = 2.5f;
    private float lowJumpMultiplier = 2f;
    private bool isFacingRight = true;
    private int jumpCount = 0;
    private int maxJumps = 2;

    void Update()
    {
        h_move = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(h_move * speed, rb.velocity.y);
        if (h_move > 0 && !isFacingRight)
        {
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            isFacingRight = true;
        }
        else if (h_move < 0 && isFacingRight)
        {
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            isFacingRight = false;
        }

        // animation
        // go
        animator.SetFloat("go", Mathf.Abs(h_move));

        // jump and double jump
        if (Input.GetButtonDown("Jump") && jumpCount < maxJumps)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            if (jumpCount == 1)
            {
                animator.SetBool("is_jump", true);
            }
            else if (jumpCount == 2)
            {
                animator.SetTrigger("double_jump");
                animator.SetBool("is_jump", true);
                animator.SetBool("is_grounded", false);
            }
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            animator.SetBool("is_grounded", false);
            animator.SetBool("is_jump", false);
            if (rb.worldCenterOfMass.y <= -2.4)
            {
                animator.SetBool("is_grounded", true);
                jumpCount = 0;
            }
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}

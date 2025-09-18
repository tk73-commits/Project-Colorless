using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpd = 5f;
    float horizontalMvmt;

    [Header("Jumping")]
    public float jumpPower = 10f;
    public int maxJumps = 2;
    int jumpsRemaining;

    [Header("GroundCheck")]
    public Transform groundCheckPos;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGrav = 2f;
    public float maxFallSpd = 10f;
    public float fallSpdMultiplier = 2f;

    void Update()
    {
        if(PauseController.IsGamePaused)
        {
            rb.velocity = Vector2.zero; // stop movement when paused
            //animator.SetBool("isWalking", false);
        }

        rb.velocity = new Vector2(horizontalMvmt * moveSpd, rb.velocity.y);
        GroundCheck();
        Gravity();

        //animator.SetBool("isWalking", rb.velocity.magnitude > 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontalMvmt = context.ReadValue<Vector2>().x;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (jumpsRemaining > 0)
        {
            if (context.performed)
            {
                //hold down = full jump height
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                jumpsRemaining--;
            }
            else if (context.canceled)
            {
                //light tap = half jump height
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                jumpsRemaining--;
            }
        }
    }

    private void GroundCheck()
    {
        if (Physics2D.OverlapBox(groundCheckPos.position, groundCheckSize, 0, groundLayer))
        {
            jumpsRemaining = maxJumps;
        }
    }

    private void Gravity() //ProcessGravity in tutorial
    {
        if(rb.velocity.y  > 0)
        {
            rb.gravityScale = baseGrav * fallSpdMultiplier; //fall increasingly faster
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpd));
        }
        else
        {
            rb.gravityScale = baseGrav;
        }
    }
    
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);
    }
}

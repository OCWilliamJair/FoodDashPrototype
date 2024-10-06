using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TemporalJump : MonoBehaviour
{
    public Transform groundCheck; 
    public LayerMask groundLayer; 
    public float groundCheckRadius = 0.2f; 
    public float jumpForceInitial = 5f;  
    public float jumpForceHold = 2f;     
    public float maxJumpHoldTime = 0.2f; 
    public float extraGravity = 2.5f;    

    public float jumpBufferTime = 0.2f;  

    private Rigidbody rb;
    private bool isGrounded;
    private bool isJumping;
    private float jumpHoldTimer;
    private float jumpBufferTimer;      
    private bool jumpBuffered;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Jump();
    }

    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump"))
        {
            jumpBuffered = true;
            jumpBufferTimer = jumpBufferTime; 
        }

        if (jumpBuffered)
        {
            jumpBufferTimer -= Time.deltaTime;

            if (jumpBufferTimer <= 0)
            {
                jumpBuffered = false;
            }
        }

        if (isGrounded && jumpBuffered)
        {
            isJumping = true;
            jumpBuffered = false;  
            jumpHoldTimer = 0f;    
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); 
            rb.AddForce(Vector3.up * jumpForceInitial, ForceMode.Impulse); 
        }

        if (isJumping && Input.GetButton("Jump"))
        {
            jumpHoldTimer += Time.deltaTime;

            if (jumpHoldTimer < maxJumpHoldTime)
            {
                rb.AddForce(Vector3.up * jumpForceHold, ForceMode.Acceleration);
            }
        }

        if (Input.GetButtonUp("Jump") || jumpHoldTimer >= maxJumpHoldTime)
        {
            isJumping = false;
        }

        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (extraGravity - 1) * Time.deltaTime;
        }

    }
    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
    


using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class TemporalMovementPlayer : MonoBehaviour
{
    public float moveSpeed = 5f;           
    public LayerMask groundLayer;
    public float jumpForce = 5f;

    private Rigidbody rb;
    private Vector3 movementInput;
    private Quaternion targetRotation;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {       
        ProcessInput();
        RotateCharacter();

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        MoveCharacter();
    }

    void ProcessInput()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        movementInput = new Vector3(moveX, 0, moveZ).normalized;
    }

    void MoveCharacter()
    {
        Vector3 moveVelocity = movementInput * moveSpeed;

        rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
    }

    void RotateCharacter()
    {
        if (movementInput != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(movementInput);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }

    void Jump()
    {       
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {       
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if ((groundLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            isGrounded = false;
        }
    }
}

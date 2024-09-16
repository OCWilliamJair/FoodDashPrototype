using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class JumpState : MonoBehaviour
{
    InputManager _inputs;
    [SerializeField] private UnityEvent OnJump;
    Rigidbody rg;
    [SerializeField] private float _jumpForce = 100;
    GroundCheck groundCheck;
    private void Awake() {
        rg = GetComponent<Rigidbody>();
        _inputs = GetComponent<InputManager>();
        groundCheck = GetComponent<GroundCheck>();
    }
    private void FixedUpdate() {
        if(_inputs.IsJumping && groundCheck.IsGrounded)
        {
            rg.AddForce(Vector3.up *_jumpForce, ForceMode.Acceleration);
            OnJump?.Invoke();
        }else{
             
        }
    }
}

using System;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof(GroundCheck))]
public class WalkState : MonoBehaviour
{
    Rigidbody rb;
    InputManager _inputs;
    Vector3 _currentDirection;
    [SerializeField] float _speed = 10;
    [SerializeField] float maxSpeed = 15;
    [SerializeField] float _rotationSpeed = 10;
    GroundCheck groundCheck;
    [SerializeField] float _velocityToWalkAgain = 0.5f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        _inputs = GetComponent<InputManager>();
        groundCheck = GetComponent<GroundCheck>();
    }
    private void Update() {
        Move();
        RotateCharacter();
    }
    void Move()
    {
        _currentDirection = new Vector3(_inputs.MovementDirection.x,0,_inputs.MovementDirection.y);
        if(_currentDirection.magnitude >= 0.1 && rb.velocity.magnitude <= maxSpeed && groundCheck.IsGrounded)
        {
            rb.AddForce( CameraRelativeDirection() * _speed, ForceMode.Force);
        }
    }
    Vector3 CameraRelativeDirection()
    {
        
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 directionToMove = (forward * _inputs.MovementDirection.y) + (right * _inputs.MovementDirection.x);
        
        return directionToMove;
    }
    void RotateCharacter()
    {
        if (_inputs.MovementDirection.magnitude > 0.1)
        {
            Quaternion tarbetRotation = Quaternion.LookRotation(CameraRelativeDirection());

            transform.rotation = Quaternion.Slerp(transform.rotation, tarbetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}

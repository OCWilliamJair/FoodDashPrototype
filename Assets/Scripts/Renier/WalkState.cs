using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof (CapsuleCollider))]
public class WalkState : MonoBehaviour
{
    Rigidbody rg;
    InputManager _inputs;
    Vector3 _currentDirection;
    [SerializeField] float _speed = 10;
    [SerializeField] float friction = 15;
    [SerializeField] float maxSpeed = 15;
    GroundCheck groundCheck;
    private void Start() {
        rg = GetComponent<Rigidbody>();
        rg.constraints = RigidbodyConstraints.FreezeRotation;
        _inputs = GetComponent<InputManager>();
        groundCheck = GetComponent<GroundCheck>();
    }
    private void Update() {
        Move();
    }
    void Move()
    {
        _currentDirection = new Vector3(_inputs.MovementDirection.x,0,_inputs.MovementDirection.y);
        if(_currentDirection.magnitude >= 0.1 && rg.velocity.magnitude <= maxSpeed && groundCheck.IsGrounded)
        {
            rg.AddForce( CameraRelativeDirection() * _speed, ForceMode.Acceleration);
        }else if (!groundCheck.IsGrounded){
            rg.velocity = Vector3.Lerp(rg.velocity,new Vector3(0,rg.velocity.y,0), Time.deltaTime);
        }else{
            rg.velocity = Vector3.Lerp(rg.velocity,new Vector3(0,rg.velocity.y,0), friction * Time.deltaTime);
        }
    }
    //Debería retornar la dirreción en World Space en la que el jugador se debe mover relativo a la camara NO ESTA LISTO
    Vector3 CameraRelativeDirection()
    {

        Vector3 directionToMove = _currentDirection;
        /*
        Vector3 forward = transform.InverseTransformVector(Camera.main.transform.forward);
        Vector3 right = transform.InverseTransformVector(Camera.main.transform.right);
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        directionToMove = (forward * _inputs.MovementDirection.y) + (right * _inputs.MovementDirection.x);
        */
        return directionToMove;
    }
}

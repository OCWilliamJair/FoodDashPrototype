using System;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class WalkState : MonoBehaviour
{
    Rigidbody rg;
    InputManager _inputs;
    Vector3 _currentDirection;
    [SerializeField] float _speed = 10;
    [SerializeField] float friction = 15;
    [SerializeField] float maxSpeed = 15;
    private void Start() {

        rg = GetComponent<Rigidbody>();
        rg.constraints = RigidbodyConstraints.FreezeRotation;
        _inputs = GetComponent<InputManager>();
    }
    private void Update() {
        _currentDirection = new Vector3(_inputs.MovementDirection.x,0,_inputs.MovementDirection.y);
        if(_currentDirection.magnitude >= 0.1 && rg.velocity.magnitude <= maxSpeed)
        {
            rg.AddForce(_currentDirection * PlayerPropiertes._currentSpeed, ForceMode.Force);
        }
        else
        {
            rg.velocity = Vector3.Lerp(rg.velocity,new Vector3(0,rg.velocity.y,0),friction *Time.deltaTime);
        }
    }
    //Debería retornar la dirreción en World Space en la que el jugador se debe mover relativo a la camara NO ESTA LISTO
    Vector3 CameraRelativeDirection()
    {
        
        Vector3 directionToMove;
        
        return Vector3.zero;
    }
}

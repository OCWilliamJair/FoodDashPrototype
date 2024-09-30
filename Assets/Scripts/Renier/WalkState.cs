using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof(GroundCheck))]
public class WalkState : MonoBehaviour
{
    Rigidbody rg;
    InputManager _inputs;
    Vector3 _currentDirection;
    [SerializeField] PlayersPropiertes _playerPropiertes;
    GroundCheck groundCheck;
    private void Start() {

        rg = GetComponent<Rigidbody>();
        rg.constraints = RigidbodyConstraints.FreezeRotation;
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
        if (_currentDirection.magnitude >= 0.1 && rg.velocity.magnitude <= _playerPropiertes._speed &&  groundCheck.IsGrounded)
        {
            rg.AddForce( CameraRelativeDirection() * _playerPropiertes._speed, ForceMode.Acceleration);
        }else{
            rg.velocity = Vector3.Lerp(rg.velocity,new Vector3(0,rg.velocity.y,0), Time.deltaTime);
        }
    }
    //Debería retornar la dirreción en World Space en la que el jugador se debe mover relativo a la camara NO ESTA LISTO
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
            Quaternion targetRotation = Quaternion.LookRotation(CameraRelativeDirection());

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _playerPropiertes._rotationSpeed);
        }
    }
}

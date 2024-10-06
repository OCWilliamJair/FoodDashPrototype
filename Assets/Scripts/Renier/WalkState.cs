using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof (CapsuleCollider))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(CustomGravity))]
[RequireComponent(typeof(PlayersPropiertes))]
public class WalkState : MonoBehaviour
{
    public Vector2 MovementDirection{get; private set;}
    Vector3 _currentDirection;
    Rigidbody rb;
    GroundCheck groundCheck;
    PlayersPropiertes _pp;
    [SerializeField] float _rotationSpeed = 10;
    [SerializeField] bool _canMoveJumping;


    public void OnMove(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        groundCheck = GetComponent<GroundCheck>();
        _pp = GetComponent<PlayersPropiertes>();
    }
    private void Update() {
        Move();
        RotateCharacter();
    }
    void Move()
    {
        _currentDirection = new Vector3(MovementDirection.x,0, MovementDirection.y);
        if(_currentDirection.magnitude >= 0.1 && rb.velocity.magnitude <= _pp.maxSpeed && (groundCheck.IsGrounded || _canMoveJumping))
        {
            rb.AddForce( CameraRelativeDirection() * _pp._speed, ForceMode.Force);
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

        Vector3 directionToMove = (forward * MovementDirection.y) + (right * MovementDirection.x);
        
        return directionToMove;
    }
    void RotateCharacter()
    {
        if (MovementDirection.magnitude > 0.1)
        {
            Quaternion tarbetRotation = Quaternion.LookRotation(CameraRelativeDirection());

            transform.rotation = Quaternion.Slerp(transform.rotation, tarbetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}

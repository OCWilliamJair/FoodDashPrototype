using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class JumpState : MonoBehaviour
{
    float _gravity;
    private float _initialJumpVelocity;
    bool _isJumping;
    [SerializeField] private float _maxJumpHeight = 1f;
    [SerializeField] private float _maxJumpTime = 0.5f;
    float _timeToApex;
    InputManager _inputs;
    [SerializeField] float _groundGravity = 0.5f;
    [SerializeField] private UnityEvent OnJump;
    Rigidbody _rb;
    GroundCheck groundCheck;
    CustomGravity customGravity;
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _inputs = GetComponent<InputManager>();
        groundCheck = GetComponent<GroundCheck>();
        customGravity = GetComponent<CustomGravity>();

        SetUpJumpVariables();
    }
    private void FixedUpdate() {
        Jump();
        ApplyCustomGravity();
    }
    void SetUpJumpVariables()
    {
        _timeToApex = _maxJumpTime/2;
        _gravity = (2 + _maxJumpHeight) / Mathf.Pow(_timeToApex, 2);
        _initialJumpVelocity = (2 * _maxJumpHeight) / _timeToApex;
    }
    void Jump()
    {
        if(!_isJumping && groundCheck.IsGrounded && _inputs.IsJumpPressed){
            _isJumping = true;
            _rb.AddForce(Vector3.up * _initialJumpVelocity * 0.5f, ForceMode.Impulse);
        }else if(_isJumping && groundCheck.IsGrounded && !_inputs.IsJumpPressed)
        {
            _isJumping = false;
        }
    }
    void ApplyCustomGravity()
    {
        bool isFalling = _rb.velocity.y < 0.0f || !_inputs.IsJumpPressed;
        float fallMultiplier = 2f;
        if(groundCheck.IsGrounded){
            customGravity.GravityScale = 1;
            customGravity.Gravity = _groundGravity;
        }
        else if(isFalling)
        {
            customGravity.GravityScale = 2;
        }
        else
        {
            customGravity.GravityScale = 1;
            customGravity.Gravity = _gravity;

        }
    }
}

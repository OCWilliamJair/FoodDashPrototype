using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(InputManager))]
public class JumpState : MonoBehaviour
{
    private float jumpBufferTimer;
    private bool jumpBuffered;
    public float jumpBufferTime = 0.2f;
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
    [SerializeField] float _fallMultiplier = 2f;
    [SerializeField] bool _applyStrongFall;
    bool IsJumpPressed{get;set;}
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
        _inputs = GetComponent<InputManager>();
        groundCheck = GetComponent<GroundCheck>();
        customGravity = GetComponent<CustomGravity>();

        SetUpJumpVariables();
    }
    public void OnJumpPressed(InputAction.CallbackContext context) 
    {
        IsJumpPressed = context.ReadValueAsButton();
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

        if(!_isJumping && groundCheck.IsGrounded && IsJumpPressed){
            _isJumping = true;
            _rb.AddForce(Vector3.up * _initialJumpVelocity * 0.5f, ForceMode.Impulse);

        }else if(_isJumping && groundCheck.IsGrounded && !IsJumpPressed)
        {
            _isJumping = false;
        }
    }
    void ApplyCustomGravity()
    {
        bool isFalling = _rb.velocity.y < 0.0f || !IsJumpPressed;
        if(groundCheck.IsGrounded){
            customGravity.GravityScale = 1;
            customGravity.Gravity = _groundGravity;
        }
        else if(isFalling)
        {
            if(_applyStrongFall)
            {
                _rb.AddForce(Vector3.down * _fallMultiplier, ForceMode.Impulse);
            }else{
                customGravity.Gravity = _gravity;
                customGravity.GravityScale = _fallMultiplier;
            }
        }
        else
        {
            customGravity.GravityScale = 1;
            customGravity.Gravity = _gravity;

        }
    }
}

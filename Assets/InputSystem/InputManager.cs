using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs _inputs;
    public Vector2 MovementDirection{get; private set;}
    public bool IsJumping { get; private set;}

    public bool IsTakenObject { get; private set;}
    private void Awake() {
        _inputs = new PlayerInputs();
        _inputs.Movement.Walk.started += OnMovement;
        _inputs.Movement.Walk.performed += OnMovement;
        _inputs.Movement.Walk.canceled += OnMovement;

        _inputs.Movement.Jump.started += OnJump;
        _inputs.Movement.Jump.canceled += OnJump;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
    void OnJump(InputAction.CallbackContext context)
    {
        IsJumping = context.ReadValueAsButton();
        Debug.Log("Jump pressed" + IsJumping);
    }

    void OnTake(InputAction.CallbackContext context)
    {
        IsTakenObject = context.ReadValueAsButton();
        Debug.Log("presionando boton F" + IsTakenObject);
    }
    private void OnEnable() {
        _inputs.Movement.Enable();
        _inputs.Actions.Enable();
    }
    private void OnDisable() {
        _inputs?.Movement.Disable();
    }
}

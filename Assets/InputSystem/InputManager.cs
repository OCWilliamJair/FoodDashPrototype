using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs _inputs;
    public Vector2 MovementDirection{get; private set;}
    public bool IsJumpPressed { get; private set;}
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
        IsJumpPressed = context.ReadValueAsButton();
    }
    private void OnEnable() {
        _inputs.Movement.Enable();
    }
    private void OnDisable() {
        _inputs?.Movement.Disable();
    }
}

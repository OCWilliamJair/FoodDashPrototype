using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    PlayerInputs _inputs;
    public Vector2 MovementDirection{get; private set;}
    public bool IsJumping { get; private set;}

    public bool IsTakenObject { get; private set;}

    public bool IsDropObject { get; private set;}

    public bool IsThrowObject { get; private set;}

    private void Awake() {
        _inputs = new PlayerInputs();
        _inputs.Movement.Walk.started += OnMovement;
        _inputs.Movement.Walk.performed += OnMovement;
        _inputs.Movement.Walk.canceled += OnMovement;

        _inputs.Actions.TakeObject.started += OnTake;
        _inputs.Actions.TakeObject.canceled += OnTake;

        _inputs.Actions.DropObject.started += OnDrop;
        _inputs.Actions.DropObject.canceled += OnDrop;
       
        _inputs.Actions.ThrowObject.started += OnThrow;
        _inputs.Actions.ThrowObject.canceled += OnThrow;

        _inputs.Movement.Walk.started += OnJump;
        _inputs.Movement.Walk.canceled += OnJump;
    }
    void OnMovement(InputAction.CallbackContext context)
    {
        MovementDirection = context.ReadValue<Vector2>();
    }
    void OnJump(InputAction.CallbackContext context)
    {
        IsJumping = context.ReadValueAsButton();
    }

    void OnTake(InputAction.CallbackContext context)
    {
        IsTakenObject = context.ReadValueAsButton();
    }

    void OnDrop(InputAction.CallbackContext context)
    {
        IsDropObject = context.ReadValueAsButton();
    }

    void OnThrow(InputAction.CallbackContext context)
    {
        IsThrowObject = context.ReadValueAsButton();
    }

    private void OnEnable() {
        _inputs.Movement.Enable();
        _inputs.Actions.Enable();
    }
    private void OnDisable() {
        _inputs.Movement.Disable();
        _inputs.Actions.Disable();
    }
}

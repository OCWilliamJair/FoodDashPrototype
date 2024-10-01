using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.InputSystem;

public class CrouchState : MonoBehaviour
{
    InputManager _inputs;
    [SerializeField] float size = 0.8f;
    bool IsShiftPressed {get;set;}
    public void OnCrouch(InputAction.CallbackContext context)
    {
        IsShiftPressed = context.ReadValueAsButton();
    }
    private void Start() {
        _inputs = GetComponent<InputManager>();
        StartCoroutine(Crouch());
    }

    IEnumerator Crouch()
    {
        while (true)
        {
            yield return new WaitUntil(()=> IsShiftPressed);
            transform.localScale = new Vector3(transform.localScale.x, size, transform.localScale.z);
            transform.position = new Vector3(transform.position.x, transform.position.y - (transform.localScale.y - size), transform.position.z );
            yield return new WaitUntil(()=> !IsShiftPressed);
            transform.localScale = new Vector3(transform.localScale.x, 1, transform.localScale.z);
        }
    }
    private void OnDisable() {
        StopAllCoroutines();   
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(Rigidbody))] 
public class CustomGravity : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 5 )] float _gravityScale = 1f;
    private float _gravity = 9.81f;
    public float GravityScale { get { return _gravityScale; } set { _gravityScale = value; } }
    Vector3 _currentGravity = Vector3.zero;
    public float Gravity {get { return _gravity;}set { _gravity = value; }}
    Rigidbody rb;
    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

    }
    private void FixedUpdate() {
        _currentGravity = _gravity * _gravityScale * Vector3.down;
        rb.AddForce(_currentGravity, ForceMode.Force);
    }
}

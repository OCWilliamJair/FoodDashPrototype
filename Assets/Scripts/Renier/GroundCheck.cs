using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float radius;
    [SerializeField] private Vector3 offset = Vector3.up;
    public bool IsGrounded { get; private set; }
    private void Update() {
        IsGrounded = Physics.CheckSphere(transform.position - offset, radius,groundLayer);
    }
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(transform.position - offset, radius);
        Gizmos.color = Color.red;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeHandle : MonoBehaviour
{
    private Rigidbody _rb;
    private RaycastHit slopeHit;
    float playerHeight;
    [SerializeField] private float maxSlopeAngle = 45;
    private void Awake() {
        _rb = GetComponent<Rigidbody>();
    }

    public bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f))
        {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }
}

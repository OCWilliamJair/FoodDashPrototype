using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTowardsMoveDirection : MonoBehaviour
{
    Vector3 previousPosition = Vector3.zero;
    private float _maxRadiansDelta = 1;
    private float _rotationSpeed = 50;
    private void Update() {
        Vector3 currentDirection = transform.position - previousPosition;
        Vector3 targetDirection = Vector3.RotateTowards(transform.forward, currentDirection, _maxRadiansDelta, Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDirection), Time.deltaTime * _rotationSpeed);
        previousPosition = transform.position;
    }
}

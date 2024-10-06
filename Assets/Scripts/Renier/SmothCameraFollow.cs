using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SmothCameraFollow : MonoBehaviour
{
    [SerializeField] GameObject max;
    [SerializeField] GameObject jax;
    Vector3 refVelovity;
    [SerializeField] float smoothTime = 0.3f;
    [SerializeField] float gizmosRadius = 0.5f;
    [SerializeField] Vector3 offset = new Vector3(-8f, 10f, -10f);
    [SerializeField] float minSize = 5; 
    [SerializeField] float  maxSize = 7;
    [SerializeField] float zoomLimiter = 13;
    private void Update() {
        MoveCamera();
        ZoomCamera();
    }
    Vector3 MiddlePoint()
    {
        return Vector3.Lerp(max.transform.position, jax.transform.position, 0.5f);
    }
    void MoveCamera()
    {
        transform.position = MiddlePoint() + offset; 
    }
    void ZoomCamera()
    {
        float distance = Vector3.Distance(max.transform.position, jax.transform.position);
        float newSize = Mathf.Lerp(minSize, maxSize, distance/zoomLimiter);
        Camera.main.orthographicSize = newSize;
    }
    private void OnDrawGizmos() {
        Gizmos.DrawSphere(MiddlePoint(),gizmosRadius);
        Gizmos.color = Color.yellow;
    }
}

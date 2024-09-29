using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] float speed = 10;
    Vector3 previousRotation = Vector3.zero;
    Rigidbody rb;
    [SerializeField] float jumpheight = 5;
    CharacterController cc;
    private void Start() {
        cc = GetComponent<CharacterController>();
    }
    void Update()
    {
        Debug.Log("Is grounded" + cc.isGrounded);
        transform.position += Vector3.forward *  speed * Time.deltaTime;
        transform.rotation = Quaternion.FromToRotation(previousRotation, transform.position);
        previousRotation = transform.position;
        if(cc.isGrounded)
        {
            cc.Move(Vector3.up * jumpheight);
        }
        
    }
}


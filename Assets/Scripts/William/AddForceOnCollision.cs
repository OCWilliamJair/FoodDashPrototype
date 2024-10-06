using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    public float pushForce = 10f;

    private void OnCollisionEnter(Collision collision)
    {       
        Rigidbody otherRigidbody = collision.collider.GetComponent<Rigidbody>();
        if (otherRigidbody != null && collision.gameObject.tag == "Player")
        {
            ContactPoint contact = collision.contacts[0];

            Vector3 pushDirection = (otherRigidbody.transform.position - contact.point).normalized;

            otherRigidbody.AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 30f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y = 0;
            rb.velocity = currentVelocity;
            
            rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceForce = 15f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            
            // 기존 y속도 제거 후 튀어오르게
            Vector3 currentVelocity = rb.velocity;
            currentVelocity.y = 0;
            rb.velocity = currentVelocity;
            
            rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);
        }
    }
}

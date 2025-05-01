using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    [Header("바람 세기 설정")]
    [SerializeField] private Vector3 windForce = new Vector3(3f, 0f, 0f); 
    [SerializeField] private ForceMode forceMode = ForceMode.Force; 

    private void OnTriggerStay(Collider other) 
    {
        Rigidbody rb = other.attachedRigidbody;  
        if (rb != null && other.CompareTag("Player"))
        {
            rb.AddForce(windForce, forceMode);
        }
    }
}

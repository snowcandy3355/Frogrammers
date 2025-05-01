using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePlatform : MonoBehaviour
{
    [Header("튕기는 힘 설정")]
    [SerializeField] private float bounceForce = 15f;
    [SerializeField] private float bounceDelay = 0.1f;

    private Dictionary<Rigidbody, float> lastBounceTimes = new Dictionary<Rigidbody, float>();

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Rigidbody rb = collision.rigidbody;
        if (rb == null) return;
        
        if (lastBounceTimes.TryGetValue(rb, out float lastTime))
        {
            if (Time.time - lastTime < bounceDelay)
                return;
        }
        
        Vector3 bounceDirection = Vector3.up;
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(bounceDirection * bounceForce, ForceMode.VelocityChange);

        lastBounceTimes[rb] = Time.time;
    }
}

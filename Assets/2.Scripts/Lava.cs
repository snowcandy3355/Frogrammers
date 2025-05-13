using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Lava : MonoBehaviour
{
    public string playerTag = "Player";
    public float timeToLaunch = 3f;
    public float jumpForce = 15f;

    [Header("Wind Settings")]
    public Vector3 windDirection = new Vector3(1, 0.2f, 1);
    public float windForce = 3f;
    public float windDuration = 1.5f;
    
    private Coroutine currentCoroutine;

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag(playerTag))
        {
            if (currentCoroutine == null)
                currentCoroutine = StartCoroutine(WaitAndLaunch(collision.collider));
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag(playerTag))
        {
            if (currentCoroutine != null)
            {
                StopCoroutine(currentCoroutine);
                currentCoroutine = null;
            }
        }
    }
    
    private IEnumerator WaitAndLaunch(Collider player)
    {
        float timer = 0f;

        while (timer < timeToLaunch)
        {
            if (player == null) yield break;
            timer += Time.deltaTime;
            yield return null;
        }

        // 1. 위로 튕기기
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z); // Y 제거
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            Debug.Log("용암에서 튕겨나감!");

            // 2. 바람 적용 코루틴 실행
            StartCoroutine(ApplyWind(rb));
        }

        currentCoroutine = null;
    }
    
    private IEnumerator ApplyWind(Rigidbody rb)
    {
        float windTime = 0f;

        while (windTime < windDuration)
        {
            if (rb == null) yield break;

            rb.AddForce(windDirection.normalized * windForce, ForceMode.Acceleration);
            windTime += Time.deltaTime;
            yield return null;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    public float launchForce = 20f;
    public float platformMoveHeight = 2f;
    public float platformMoveSpeed = 10f;

    private Vector3 originalPosition;
    private bool isLaunching = false;

    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isLaunching) return;

        if (collision.collider.CompareTag("Player"))
        {
            Rigidbody playerRb = collision.collider.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                isLaunching = true;

                // 1. 플레이어 튕기기
                playerRb.velocity = new Vector3(playerRb.velocity.x, 0, playerRb.velocity.z); // Y속도 초기화
                playerRb.AddForce(Vector3.up * launchForce, ForceMode.VelocityChange);

                // 2. 플랫폼도 순간 위로 튀기기
                StartCoroutine(PopPlatform());
            }
        }
    }

    private IEnumerator PopPlatform()
    {
        transform.position = originalPosition + Vector3.up * platformMoveHeight;
        yield return new WaitForSeconds(0.1f);
        transform.position = originalPosition;
        isLaunching = false;
    }
}

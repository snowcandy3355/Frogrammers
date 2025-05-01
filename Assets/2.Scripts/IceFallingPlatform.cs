using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceFallingPlatform : MonoBehaviour
{
    public float shakeDuration = 0.5f; // 떨림 지속 시간
    public float shakeMagnitude = 0.1f; // 떨림 강도
    public float delayBeforeFall = 0.2f; // 떨림 후 낙하까지 딜레이
    public float returnDelay = 5f;         // 원위치 복귀까지 대기 시간
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Rigidbody rb;
    private bool triggered = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // 처음엔 고정된 상태
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        
        MeshCollider mc = GetComponent<MeshCollider>();
        mc.convex = true; // 반드시 Convex여야 함
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!triggered && collision.collider.CompareTag("Player"))
        {
            triggered = true;
            StartCoroutine(ShakeAndFall());
        }
    }

    IEnumerator ShakeAndFall()
    {
        float timer = 0f;

        while (timer < shakeDuration)
        {
            Vector3 offset = Random.insideUnitSphere * shakeMagnitude;
            transform.position = originalPosition + offset;

            timer += Time.deltaTime;
            yield return null;
        }

        // 떨림 끝났으면 원래 위치로 복귀
        transform.position = originalPosition;
        yield return new WaitForSeconds(delayBeforeFall);

        rb.isKinematic = false; // 중력 적용 → 낙하 시작
        
        // 3. 5초 후 초기화
        yield return new WaitForSeconds(returnDelay);

        // 초기화: 위치 리셋 + 속도 리셋 + 고정
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.isKinematic = true;
        
        transform.position = originalPosition;
        transform.rotation = originalRotation;

        // 4. 다시 작동 가능하도록 상태 복구
        triggered = false;
    }
}
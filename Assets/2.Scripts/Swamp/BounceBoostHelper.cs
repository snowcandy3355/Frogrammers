using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBoostHelper : MonoBehaviour
{
    public float boostDuration = 1f;
    public float airControlSpeed = 3f;

    private float boostTimer = 1f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        enabled = false; // 기본 비활성화
    }

    void Update()
    {
        if (boostTimer > 0)
        {
            boostTimer -= Time.deltaTime;

            // 방향 입력 받기
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Vector3 move = new Vector3(h, 0, v).normalized;

            if (move.magnitude > 0.1f)
            {
                // 카메라 기준으로 방향 전환
                Camera cam = Camera.main;
                Vector3 camForward = cam.transform.forward;
                Vector3 camRight = cam.transform.right;
                camForward.y = 0;
                camRight.y = 0;
                move = camRight * h + camForward * v;

                rb.AddForce(move.normalized * airControlSpeed, ForceMode.Acceleration);
            }
            
            if (boostTimer <= 0f)
            {
                enabled = false; // 자동으로 꺼지기
            }
        }
    }

    public void ActivateBoost()
    {
        boostTimer = boostDuration;
        enabled = true;
    }
}

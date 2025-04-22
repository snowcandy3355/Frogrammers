using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    //개구리 이동속도
    [SerializeField]private float moveSpeed = 3f;
    //메인 카메라
    [SerializeField] Camera playerCamera;
    //개구리 프리팹
    [SerializeField] private Transform player;
    private float cameraRotationSpeed = 3;
    private float mouseX, mouseY;
    private Rigidbody rb;
    private Vector3 cameraOffset = new Vector3(0, 2, -3);

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerMovement();
        CameraMovement();
    }

    #region 플레이어 동작
    private void PlayerMovement()
    {
        // 키보드 입력감지
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        // 마우스 입력 감지 * 마우스 회전속도
        mouseX += Input.GetAxis("Mouse X")*cameraRotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y")*cameraRotationSpeed;
        //카메라의 최대 각도 설정
        mouseY = Mathf.Clamp(mouseY, -90, 50);
        //카메라의 방향을 정의하고 y값을 초기화
        Vector3 cameraForward = playerCamera.transform.forward;
        Vector3 cameraRight = playerCamera.transform.right;
        cameraForward.y = 0;
        cameraRight.y = 0;
        //플레이어의 움직임과 카메라의 방향 동기화
        Vector3 movement = (cameraRight * moveHorizontal + cameraForward * moveVertical);
        movement.Normalize();
        //카메라의 전방과 플레이어 오브젝트의 전방을 동기화하는 변수
        Quaternion playerRotation = Quaternion.Euler(0,mouseX,0);
        transform.rotation = playerRotation;
        //플레이어 이동 + 회전
        Vector3 newPosition = rb.position + movement * (moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);
    }

    private void CameraMovement()
    {
        //카메라 회전 계산
        Quaternion targetRotation = Quaternion.Euler(mouseY, mouseX, 0);
        //카메라 위치 선정
        playerCamera.transform.position = player.position  + targetRotation * cameraOffset;
        //카메라의 방향을 타겟의 포지션에 고정
        playerCamera.transform.LookAt(player.position);
    }
    #endregion
    
    
}

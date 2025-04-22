using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RopeAction : MonoBehaviour
{
    //연결 가능한 오브젝트
    [SerializeField] LayerMask mapObj;
    //개구리 프리팹
    [SerializeField] private Transform _player;
    //메인 카메라
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float _hookSpeed;
    private RaycastHit _raycastHit;
    private LineRenderer _lineRenderer;
    private bool _isGrappling;
    private SpringJoint _springJoint;
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _rigidbody = GetComponentInParent<Rigidbody>();
        _lineRenderer.positionCount = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckRaycastAndShootRope();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeleteRope();
        }
        
        OnDrawFollowingRope();
        
        if (Input.GetMouseButtonDown(1))
        {
            BoostToEndOfRope();
        }
        
        //커서 띄우고 없애기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    #region 혀 동작부

    //RayCast 이 후 LineRenderer를 이용해 줄을 연결하는 메서드
    private void CheckRaycastAndShootRope()
    {
        if (Physics.Raycast(transform.position, playerCamera.transform.forward, out _raycastHit, 15f, mapObj))
        {
            _isGrappling = true;
            Debug.Log("Raycast Hit");
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _raycastHit.point);

            _springJoint = _player.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = _raycastHit.point;

            //줄의 길이 정의
            float dis = Vector3.Distance(this.transform.position, _raycastHit.point);
            _springJoint.maxDistance = dis;
            _springJoint.minDistance = dis * 0.5f;
            
            //통통 튀는 느낌 제어
            _springJoint.damper = 20f;
            //장력
            _springJoint.spring = 100f;
            //질량 비율
            _springJoint.massScale = 1f;

        }
    }

    //연결되있던 LineRenderer 삭제하는 메서드
    private void DeleteRope()
    {
        _isGrappling = false;
        _lineRenderer.positionCount = 0;

        if (_springJoint != null)
        {
            Destroy(_springJoint);
            _springJoint = null;
        }
        
    }

    //플레이어에 따라 줄을 업데이트 해주는 함수
    private void OnDrawFollowingRope()
    {
        if (_isGrappling && _lineRenderer.positionCount >= 2)
        {
            _lineRenderer.SetPosition(0, transform.position);
        }
    }
    
    //우클릭 시 개구리가 혀의 연결점 끝으로 힘을 받아서 날아감
    private void BoostToEndOfRope()
    {
        var pos = _raycastHit.point;
        _rigidbody.AddForce(_raycastHit.point * _hookSpeed, ForceMode.Impulse);
    }

    #endregion
    
}

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
    //크로스헤어 이미지
    [SerializeField] private GameObject blackCrossHair;
    [SerializeField] private GameObject redCrossHair;
    [SerializeField] private float _hookSpeed;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private Animator _animator;
    private RaycastHit _raycastHit;
    private LineRenderer _lineRenderer;
    private bool _isGrappling;
    private SpringJoint _springJoint;
    private Rigidbody _rigidbody;
    
    public bool IsGrappling => _isGrappling;
    
    
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
            _animator.SetTrigger("Grappling");
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            DeleteRope();
        }
        
        OnDrawFollowingRope();
        
        if (Input.GetMouseButton(1))
        {
            BoostToEndOfRope();
        }
    }

    #region 혀 동작부

    //RayCast 이 후 LineRenderer를 이용해 줄을 연결하는 메서드
    private void CheckRaycastAndShootRope()
    {
        //_aimedTarget.Normalize();
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out _raycastHit, 30f, mapObj))
        {
            //라인 렌더러의 시작점
            Vector3 from = playerCamera.transform.position;
            //라인 렌더러의 끝점
            Vector3 to = _raycastHit.point;
            _isGrappling = true;
            _lineRenderer.positionCount = 2;
            _lineRenderer.SetPosition(0, from);
            _lineRenderer.SetPosition(1, to);

            _springJoint = _player.gameObject.AddComponent<SpringJoint>();
            _springJoint.autoConfigureConnectedAnchor = false;
            _springJoint.connectedAnchor = _raycastHit.point;

            //줄의 시작점과 끝점의 거리 정의
            float dis = Vector3.Distance(this.transform.position, _raycastHit.point);
            _springJoint.minDistance = 0;
            _springJoint.maxDistance = dis;
            
            //통통 튀는 느낌 제어
            _springJoint.damper = 20f;
            //장력
            _springJoint.spring = 100f;
            //질량 비율
            _springJoint.massScale = 1f;

        }
        else
        {
            StartCoroutine(ChangeCrossHair());
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
        if (_isGrappling)
        {
            Vector3 direction = (_raycastHit.point - _player.position).normalized;
            _rigidbody.AddForce(direction * _hookSpeed, ForceMode.Acceleration);
        }
    }

    #endregion

    IEnumerator ChangeCrossHair()
    {
        blackCrossHair.SetActive(false);
        redCrossHair.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackCrossHair.SetActive(true);
        redCrossHair.SetActive(false);
    }
}

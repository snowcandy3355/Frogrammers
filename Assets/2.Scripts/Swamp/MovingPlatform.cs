using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("이동 설정")]
    [SerializeField] private Vector3 moveOffset = new Vector3(5, 0, 0); 
    [SerializeField] private float moveSpeed = 2f; 
    
    private Vector3 startPos; 
    private Vector3 targetPos; 
    private bool movingToTarget = true;

    private void Start()
    {
        startPos = transform.position - moveOffset * 0.5f; //시작점
        targetPos = transform.position + moveOffset * 0.5f; //도착점
    }

    private void Update()
    {
        // 목적지로 이동
        Vector3 destination = movingToTarget ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // 방향 전환
        if (Vector3.Distance(transform.position, destination) < 0.05f)
        {
            movingToTarget = !movingToTarget;
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(this.transform);
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null); 
        }
    }
}

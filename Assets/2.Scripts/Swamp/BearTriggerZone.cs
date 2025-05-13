using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTriggerZone : MonoBehaviour
{
    private bool playerInside = false;
    private Coroutine spawnRoutine;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BearObstacle spawner = GetComponentInParent<BearObstacle>();
            if (spawner != null)
            {
                spawner.StartSpawning(); //스폰 실행
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            BearObstacle spawner = GetComponentInParent<BearObstacle>();
            if (spawner != null)
            {
                spawner.StopSpawning(); // 스폰 중지
            }
        }
    }
}

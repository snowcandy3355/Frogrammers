using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BearMove : MonoBehaviour
{
    private NavMeshAgent agent;
    public float destroyAfter = 10f;

    public void Init(Vector3 destination, float speed, float lifetime)
    {
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.SetDestination(destination);

        Destroy(gameObject, lifetime); 
    }
}

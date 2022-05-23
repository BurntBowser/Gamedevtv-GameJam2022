using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    
    [SerializeField] Vector3 position;
    NavMeshAgent agent;

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void OnEnable() 
    {
        agent.SetDestination(position);
    }
}

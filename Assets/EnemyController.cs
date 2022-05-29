using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    [SerializeField] GameObject startPos;
    NavMeshAgent agent;
    [SerializeField] GameObject endPos;
    public bool checkStatus = false;
    [SerializeField]  bool isStopped = false;
    

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = isStopped;
    }

    void OnEnable() 
    {
        startPos = GameObject.Find("SpawnPoint");
        endPos = GameObject.Find("EndPoint");
        transform.position = startPos.transform.position;
        StartCoroutine(getPath()); //move path drawing to new object.
    }

    void Update() 
    {
        CanReachPosition(transform.position);
    }

    public void CanReachPosition(Vector3 position)
    {
        agent.CalculatePath(position, agent.path);
        if (agent.path.status == NavMeshPathStatus.PathComplete)
        {
            checkStatus = true;
            return;
        }
        else
        {
            checkStatus = false;
            //destroy towers
        }
    }

    IEnumerator getPath()
    {      
        agent.SetDestination(endPos.transform.position);       
        yield return new WaitForEndOfFrame();
        
    }

}

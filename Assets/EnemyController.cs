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
    public int TotalHealth = 10;
    public int reward;
    public int lifeLost;
    public bool reachedEnd = false;
    PlayerStats stats;
    private bool isAlive = true;

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = isStopped;
        stats = FindObjectOfType<PlayerStats>();
        agent.ResetPath();
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
        if(Vector3.Distance(transform.position,agent.destination)<=1f)
        {
            reachedEnd = true;
            Destroy(gameObject);
            stats.Lives -=lifeLost;
        }
        if(TotalHealth<=0)
        {
            isAlive= false;
            Destroy(gameObject);
            return;
        }
        if(isAlive == true && agent.isStopped==true && Vector3.Distance(transform.position,agent.destination)>=1f)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position,3f);
            foreach(Collider collider in colliders)
            {
                if(collider.tag == "Tower")
                {
                    Destroy(collider);
                }
            }
        }

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

    private void OnDestroy() 
    {
        if(reachedEnd == true){return;}
        stats.GiveReward(reward);
    }

    IEnumerator getPath()
    {      
        agent.SetDestination(endPos.transform.position);       
        yield return new WaitForEndOfFrame();
        
    }

}

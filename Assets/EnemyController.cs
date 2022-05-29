using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Vector3 startPos;
    NavMeshAgent agent;
    LineRenderer line;
    [SerializeField] Vector3 waypoint;
    public bool checkStatus = false;
    

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();
        //agent.isStopped = true;
        //add scene index checks to load proper coordinates
    }

    void OnEnable() 
    {
        transform.position = startPos;
        StartCoroutine(getPath()); //move path drawing to new object.
    }

    void Update() 
    {
        DrawPath(agent.path);
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
        
        line.SetPosition(0,transform.position);        
        agent.SetDestination(waypoint);       
        yield return new WaitForEndOfFrame();
        
    }

    void DrawPath(NavMeshPath path)
    {
        if(path.corners.Length<2) {return;}
        line.positionCount = path.corners.Length;
        line.SetPositions(path.corners);

        for(int i=0; i<path.corners.Length; i++)
        {
            line.SetPosition(i,path.corners[i]);
        }
        
    }
}

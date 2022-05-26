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
    [SerializeField] Transform target;


    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();
    }

    void OnEnable() 
    {
        transform.position = startPos;
        StartCoroutine(getPath()); //move path drawing to new object.
    }

    void Update() 
    {
        //redraw path if changed
    }

    IEnumerator getPath()
    {
        line.SetPosition(0,transform.position);        
        agent.SetDestination(target.position);
        yield return new WaitForEndOfFrame();

        DrawPath(agent.path);
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

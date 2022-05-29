using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class DrawColoredPath: MonoBehaviour
{
    [SerializeField] GameObject startPos;
    NavMeshAgent agent;
    LineRenderer line;
    [SerializeField] GameObject endPos;
    public bool checkStatus = false;
    [SerializeField]  bool isStopped = true;
    

    void Awake() 
    {
        agent = GetComponent<NavMeshAgent>();
        line = GetComponent<LineRenderer>();
        agent.isStopped = isStopped;
        
        if(SceneManager.GetActiveScene().name == "Map 1")
        {
            startPos = new GameObject("SpawnPoint"); 
            startPos.transform.position = new Vector3(-25,0,25);
            endPos = new GameObject("EndPoint"); 
            endPos.transform.position = new Vector3(25,0,-25);
            
        }

    }

    void OnEnable() 
    {
        transform.position = startPos.transform.position;
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
            Debug.Log("Path has been obstruted, allies will be killed");
        }
    }

    IEnumerator getPath()
    {
        
        line.SetPosition(0,transform.position);        
        agent.SetDestination(endPos.transform.position);       
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

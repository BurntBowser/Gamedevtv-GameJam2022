using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent (typeof (AI_Offmesh))]
public class AI_Brain : MonoBehaviour {

	public GameObject waypointManager;
	public PathBuilder_FS myPathBuilder;
	public List<GameObject> aiPathPoints;
	public int destPoint = 0;
	public NavMeshAgent agent;



 void Start () {
		agent = GetComponent<NavMeshAgent>();
		myPathBuilder = waypointManager.GetComponent<PathBuilder_FS> ();

		agent.autoBraking = false;

		GoToNextPoint ();



	}



	 void GoToNextPoint() {
		
		//Returns if no points have been setup
		if (myPathBuilder.CreatedPaths.Count == 0) 
			return;

			if(destPoint<myPathBuilder.CreatedPaths.Count)
			{
			//Set the agent to go to the currently selected destination
			agent.destination = myPathBuilder.CreatedPaths [destPoint].transform.position;

			//Choose the next point in the list as the destination, cycling to the start if required.
			destPoint = (destPoint + 1);
			}
			else
			{return;}


		}






	void Update() {

		//Choose the next destination point when the agents gets close the current one, and stop when agent stops
		if (!agent.pathPending && agent.remainingDistance < 0.5f && agent.isStopped!=true) {
			GoToNextPoint ();
		}
	}

	

	
	 
   
}







	




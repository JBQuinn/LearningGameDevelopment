using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//Do not allow the script to run unless there is a NavMeshAgent component
[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour {

	Transform target;		//The target of the player
	NavMeshAgent agent;		//The navigation agent to identify where the player can move
	float lookSpeed = 5f;	//The speed at which the player turns towards their target

	// Use this for initialization
	void Start () {

		//Get the navigation mesh and assign it to the navigation agent
		agent = GetComponent<NavMeshAgent>();

	}

	void Update () {

		//If the player has a target
		if ( target != null ) {

			//Set the destination on the nav mesh to the target's position
			agent.SetDestination ( target.position );
			//Have the player face their target
			FaceTarget ();

		}

	}
	
	// Update is called once per frame
	public void MoveToPoint (Vector3 point) {

		//Set the destination to the target's position (incase it has moved)
		agent.SetDestination(point);

	}

	public void FollowTarget ( Interactable newTarget ) {

		//Set the stopping distance from the target to just inside the target's radius
		agent.stoppingDistance = newTarget.radius * 0.8f;
		//Don't have the nav mesh update the rotation since we are doing it in the FaceTarget method
		agent.updateRotation = false;

		//Set the target to the new target's interaction transform object
		target = newTarget.interactionTransform;

	}

	public void StopFollowingTarget () {

		//Set the stopping distance to 0
		agent.stoppingDistance = 0f;
		//Have the nav mesh update the rotation
		agent.updateRotation = true;

		//Set the target to nothing
		target = null;
		
	}

	void FaceTarget () {

		//The vector towards target of the player
		Vector3 direction = ( target.position - transform.position ).normalized;
		//The rotation of the player towards the target but does not tilt them towards the target
		Quaternion lookRotation = Quaternion.LookRotation ( new Vector3 ( direction.x, 0f, direction.z ) );
		//Rotates the player towards the target at the lookspeed pre-defined by the user
		transform.rotation = Quaternion.Slerp ( transform.rotation, lookRotation, Time.deltaTime * lookSpeed );

	}

}
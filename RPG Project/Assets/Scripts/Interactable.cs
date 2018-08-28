using UnityEngine;

public class Interactable : MonoBehaviour {

	public float radius = 3f;				//The radius in which the player can interact with the interactable

	public Transform interactionTransform;	//The transform of where you want the player to interact with the interactable

	bool isFocus = false;					//A boolean of whether the interactable is the player's current focus
	Transform player;						//The transform of the player
	bool hasInteracted = false;				//A boolean of whether the player has interacted with the interactable

	public virtual void Interact () {
		//This method is meant to be overwritten

		//Identifies what is being interacted with and is used to debug when a player is interacting with an interactable
		print ( "Interacting with " + interactionTransform.name );
	}

	void Update () {

		//if the interactable is the player's focus and the player has not yet interacted with it
		if ( isFocus && !hasInteracted ) {
			//Find the distance between the player and the location they need to be to interact with the interactable
			float distance = Vector3.Distance ( player.position, interactionTransform.position );
			//if the distance is less than the radius we defined as being sufficient to interact with the interactable
			if ( distance <= radius ) {
				//Run the interact method
				Interact ();
				//set the flag to indicate that we have interacted with the interactable
				hasInteracted = true;
			}
		}
	}

	public void OnFocused ( Transform playerTransform ) {

		//Set the interactable as our player's focus
		isFocus = true;
		//Set the player's transform
		player = playerTransform;
		//Set the flag to indicate that we have not yet interacted with the interactable since clicking on it
		hasInteracted = false;

	}

	public void OnDefocused () {

		//Set the interactable as no longer being our player's focus
		isFocus = false;
		//Set the player's transform as null. The interactable no longer needs to know where the player is at in the world
		player = null;
		//Set the flag to indicate that the player has no longer interacted with the interactable
		hasInteracted = false;

	}

	void OnDrawGizmosSelected () {

		//Set the gizmo's color to yellow
		Gizmos.color = Color.yellow;
		//Draw a WireSphere gizmo around the interaction location with a radius of the interaction radius
		Gizmos.DrawWireSphere ( interactionTransform.position, radius );

	}

}
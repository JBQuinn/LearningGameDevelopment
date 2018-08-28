using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Requires there to be a PlayerMotor script to implement this script
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	//Public Variables
	public LayerMask movementMask;	//The layer on which the player can move. Layer's are set and named in the Unity editor
	public Interactable focus;		//The interactable that the player has in focus

	//Private variables
	Camera cam;						//A reference to the main camera
	PlayerMotor motor;				//A reference to the PlayerMotor script


	// Use this for initialization
	void Start () {

		cam = Camera.main; 						//Sets the Player Controller's camera variable to the main camera of the game
		motor = GetComponent<PlayerMotor>();	//Sets the PlayerMotor script reference to the PlayerMotor Component

	}
	
	// Update is called once per frame
	void Update () {

		//If the player has left clicked
		if ( Input.GetMouseButtonDown( 0 ) ) {

			//Draw a ray from the camera to the point the mouse is at
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );
			//A variable to reference the point at which the ray hits the layer
			RaycastHit hit;

			//If the ray hits the movementMask layer within 100 meters of the camera
			if ( Physics.Raycast( ray, out hit, 100, movementMask ) ) {

				// Move our player to what we hit
				motor.MoveToPoint( hit.point );
				// Stop focus on objects
				RemoveFocus ();

			}

		//If the player has right clicked
		} else if ( Input.GetMouseButtonDown( 1 ) ) {

			//Draw a ray from the camera to the point the mouse is at
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );
			//A variable to reference the point at which the ray hits the layer
			RaycastHit hit;

			//If the ray hits anything within 100 meters of the camera
			if ( Physics.Raycast( ray, out hit, 100 ) ) {

				//A variable that stores the interactable component of the item the ray has hit
				Interactable interactable = hit.collider.GetComponent<Interactable> ();
				//If the interactable component has a value then the player has right-clicked on an interactable object
				if ( interactable != null ) {
					
					//Set the player's focus to the interactable
					SetFocus(interactable);

				}

			}

		}

	}

	//Set the player's focus to what we right clicked on
	void SetFocus ( Interactable newFocus ) {

		//if the item we are focusing on is not what we had focus on
		if ( newFocus != focus ) {

			//If the item we were focusing on  was an item
			if ( focus != null )
				//Defocus on the old focus
				focus.OnDefocused();

			//Set the current focus to the new item
			focus = newFocus;
			//Have the motor follow our newest focus
			motor.FollowTarget ( focus );

		}

		//Run the onFocused method of the interactable with the players transform
		focus.OnFocused ( transform );

	}

	//Remove the current focus from the player's focus
	void RemoveFocus () {

		//If there is a current focus
		if ( focus != null )
			//Run the defocus of the current focus
			focus.OnDefocused();

		//set the current focus to null
		focus = null;
		//Have the player stop following their focus
		motor.StopFollowingTarget ();

	}
	
}
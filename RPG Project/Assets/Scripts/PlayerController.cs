using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	public LayerMask movementMask;
	Camera cam;
	PlayerMotor motor;

	public Interactable focus;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		if ( Input.GetMouseButtonDown( 0 ) ) {
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if ( Physics.Raycast( ray, out hit, 100, movementMask ) ) {
				// Move our player to what we hit
				motor.MoveToPoint( hit.point );
				// Stop focus on objects
				RemoveFocus ();
			}
		} else if ( Input.GetMouseButtonDown( 1 ) ) {
			Ray ray = cam.ScreenPointToRay( Input.mousePosition );
			RaycastHit hit;

			if ( Physics.Raycast( ray, out hit, 100 ) ) {
				Interactable interactable = hit.collider.GetComponent<Interactable> ();
				if ( interactable != null ) {
					SetFocus(interactable);
				}
				// Check if we hit an interactable. If yes, make it a focus
			}
		}
	}

	void SetFocus ( Interactable newFocus ) {

		if ( newFocus != focus ) {

			if ( focus != null )
				focus.OnDefocused();

			focus = newFocus;
			motor.FollowTarget ( focus );

		}

		focus.OnFocused ( transform );

	}

	void RemoveFocus () {

		if ( focus != null )
			focus.OnDefocused();

		focus = null;
		motor.StopFollowingTarget ();

	}
	
}

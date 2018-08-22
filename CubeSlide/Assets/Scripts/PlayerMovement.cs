using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//This is a reference to the Player's RigidBody component
	public Rigidbody rb;
	public float forwardForce = 2000f;
	public float sidewaysForce = 500f;
	public float jumpForce = 100f;
	public bool left = false;
	public bool right = false;
	public bool jump = false;
	public bool onGround = true;
	
	// This is using FixedUpdate to apply fixes after
	// a set number of frames instead of after every frame
	void FixedUpdate () {
		//Applying a force on the z, based on how much time has passed
		rb.AddForce(0,0,forwardForce * Time.deltaTime);

		if ( right ) {
			rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0);
			right = false;
		}
		if ( left ) {
			rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0);
			left = false;
		}
		if ( jump ) {
			rb.AddForce(0, jumpForce * Time.deltaTime, 0);
			jump = false;
		}

	}

	//Check if the keys to move have been pressed
	void Update () {
		if ( Input.GetKey("d") && onGround ) {
			right = true;
		}
		if ( Input.GetKey("a") && onGround ) {
			left = true;
		}
		if ( Input.GetKey("space") && onGround ) {
			jump = true;
		}
	}

	//The player has collided with the ground
	void OnCollisionEnter ( Collision collision) {
		if ( collision.gameObject.tag == "Ground" ) {
			onGround = true;
		}
	}

	void OnCollisionExit ( Collision collision ) {
		if ( collision.gameObject.tag == "Ground" ) {
			onGround = false;
		}
	}

}

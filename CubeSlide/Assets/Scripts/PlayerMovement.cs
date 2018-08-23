using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	//This is a reference to the Player's GameObject
	public GameObject player;
	//This is a reference to the Player's RigidBody component
	public Rigidbody rb;
	public float forwardForce = 2000f;
	public float acceleration = 1f;
	private float maxForwardForce;
	public float sidewaysForce = 500f;
	public float jumpForce = 100f;
	public bool left = false;
	public bool right = false;
	public bool jump = false;
	public bool onGround = true;
	
	void Start () {
		rb = player.GetComponent<Rigidbody>();
		maxForwardForce = forwardForce * 10f;
	}

	// This is using FixedUpdate to apply fixes after
	// a set number of frames instead of after every frame
	void FixedUpdate () {
		//Increase forward force
		forwardForce = Mathf.Clamp((forwardForce+acceleration),0, maxForwardForce);
		//Applying a force on the z, based on how much time has passed
		rb.AddForce(0,0,forwardForce * Time.deltaTime);
		if ( right ) {
			player.transform.Translate(sidewaysForce * Time.deltaTime, 0, 0);
		} else if ( left ) {
			player.transform.Translate(-sidewaysForce * Time.deltaTime, 0, 0);
		}
		if ( jump ) {
			rb.AddForce(0, jumpForce * Time.deltaTime, 0);
		}

		if ( rb.position.y < 0f) {
			FindObjectOfType<GameManager>().EndGame();
		}

	}

	//Check if the keys to move have been pressed
	void Update () {
		if ( Input.GetKey("d") && onGround ) {
			right = true;
		} else {
			right = false;
		}
		if ( Input.GetKey("a") && onGround ) {
			left = true;
		} else {
			left = false;
		}
		if ( Input.GetKey("space") && onGround ) {
			jump = true;
		} else {
			jump = false;
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

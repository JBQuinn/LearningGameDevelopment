using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target; 			//A reference to the transform of the target of the camera (primarily the player)
	public Vector3 offset; 				//The offset from the target to the camera
	public float zoomSpeed = 4f;		//The the number of meters the zoom should update by for each change of the mouse scrollwheel
	public float minZoom = 5f; 			//The smallest number of offsets the camera can get from the player
	public float maxZoom = 15f; 		//The largest number of offsets the camera can get from the player
	public float pitch;					//The pitch of the camera
	public float yawSpeed = 100f;		//The speed at which the camera rotates around the player

	//Private Variables
	private float currentZoom = 10f;	//preset zoom distance for the camera. Currently set to 10 units behind the player
	private float currentYaw = 0f; 		//preset yaw of the camera. Currently set to 0 to be directly behind the player

	void Update () {

		//changes the zoom on the camera based on the amount of scrolling on the mouse scroll wheel
		currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
		currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

		//changes the yaw based on how much the horizontal axis buttons have been pressed
		currentYaw -= Input.GetAxis("Horizontal") * yawSpeed * Time.deltaTime;

	}

	void LateUpdate () {

		//Updates the position of the camera based on the player's movement
		transform.position = target.position - offset * currentZoom;
		//Keeps the camera looking at the player
		transform.LookAt(target.position + Vector3.up * pitch);

		//Rotates the camera around the player based on the current yaw calculated in the update method
		transform.RotateAround(target.position, Vector3.up, currentYaw);

	}

}
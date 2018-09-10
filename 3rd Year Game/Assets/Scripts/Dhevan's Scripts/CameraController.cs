using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraController : MonoBehaviour
{

	private InputDevice controller;

	public Transform playerT;
	public float followTime = 0.15f;
	public float FollowSpeed = 4f;

	public float xPosOffset = 0f, yPosOffset = 3.5f, zPosoffset = -12f;

	private bool cameraFlipped = false;
	private bool cameraFlipping = false;
	private float startFlipTime;

	private Vector3 camRotation;
	public float camFlipSpeed = 6f;

	// Use this for initialization
	void Start ()
	{
		controller = InputManager.ActiveDevice;
		camRotation = new Vector3 (this.transform.eulerAngles.x, 0f, this.transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Update Camera Position
		Vector3 newPosition = new Vector3 (playerT.position.x + xPosOffset, playerT.position.y + yPosOffset, playerT.position.z + zPosoffset);
		this.transform.position = Vector3.Slerp (transform.position, newPosition, FollowSpeed * Time.deltaTime);

		//Update Camera Rotation
		Quaternion rot = Quaternion.Euler (camRotation);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * camFlipSpeed);


		if ((Input.GetKeyDown(KeyCode.Q) || controller.Action4) && cameraFlipping == false) {
			FlipCamera ();
			GameObject.Find ("Player").SendMessage ("InvertControls");
			cameraFlipping = true;
			startFlipTime = Time.time;
		}

		if (Time.time >= startFlipTime + 1f) {
			cameraFlipping = false;

		if (Input.GetKeyDown(KeyCode.Q) || controller.Action4) {
			FlipCamera ();
			GameObject.Find ("Player").SendMessage ("InvertControls");

		}

		}
	}

	void FlipCamera ()
	{

		if (cameraFlipped == false) {
			camRotation = new Vector3 (this.transform.eulerAngles.x, 180f, this.transform.eulerAngles.z);
			cameraFlipped = true;

		} else {
			camRotation = new Vector3 (this.transform.eulerAngles.x, 0f, this.transform.eulerAngles.z);
			cameraFlipped = false;
		}
		zPosoffset = zPosoffset * -1f;

	}
}


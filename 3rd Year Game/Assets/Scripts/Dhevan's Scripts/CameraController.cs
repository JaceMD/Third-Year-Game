using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CameraController : MonoBehaviour
{

	private InputDevice controller;

	public Transform playerT, supportT;
	private Transform focalObjT;
	private bool camOnPlayer = true;
	public float followTime = 0.15f;
	public float FollowSpeed = 4f;
	public float xPosOffset = 0f, yPosOffset = 3.5f, zPosoffset = -12f;

	private bool cameraFlipped = false;
	private bool cameraFlipping = false;
	private float startFlipTime;
	private Vector3 camRotation;
	public float camFlipSpeed = 6f;

	private bool buttonPressDelay = false;
	private float startButtonPressDelay; //Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	// Use this for initialization
	void Start ()
	{
		focalObjT = playerT;
		controller = InputManager.ActiveDevice;
		camRotation = new Vector3 (this.transform.eulerAngles.x, 0f, this.transform.eulerAngles.z);
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Update Camera Position
		Vector3 newPosition = new Vector3 (focalObjT.position.x + xPosOffset, focalObjT.position.y + yPosOffset, focalObjT.position.z + zPosoffset);
		this.transform.position = Vector3.Slerp (transform.position, newPosition, FollowSpeed * Time.deltaTime);

		//Update Camera Rotation
		Quaternion rot = Quaternion.Euler (camRotation);
		this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rot, Time.deltaTime * camFlipSpeed);

		/*
		if ((Input.GetKeyDown(KeyCode.Q) || controller.Action4) && cameraFlipping == false) {
			FlipCamera ();
			GameObject.Find ("Player").SendMessage ("InvertControls");
			cameraFlipping = true;
			startFlipTime = Time.time;
		}

		if (Time.time >= startFlipTime + 1f) {
			cameraFlipping = false;
		}
		*/
		if (controller.RightStickButton && buttonPressDelay == false) {
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
			if (camOnPlayer == true) {
				camOnPlayer = false;
				focalObjT = supportT;
			}
			else if(camOnPlayer == false){
				focalObjT = playerT;
				camOnPlayer= true;
			}
		}

		if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
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

	void setFocalObject(int obj){
		//1 = player
		//2 = support character
		if (obj == 1) {
			focalObjT = playerT;
		}
		else if(obj == 2){
			focalObjT = supportT;
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SupportCharController : MonoBehaviour {

	private InputDevice controller;
	public Transform playerT;
	public float xPosMaxOffset, yPosMaxOffset, zPosMaxOffset;
	public float suppCharFollowSpeed = 4f;
	private  bool followMode = true;

	private float xInput, yInput, zInput;
	private bool controlsInverted = false;
	private bool controlsDisabled = false;

	public float moveSpeed = 10f;

	public GameObject playerObj;
	public GameObject playerRCTarget1, playerRCTarget2, playerRCTarget3, playerRCTarget4;

	private Ray[] lightDetectionRays = new Ray[4];
	private GameObject[] pRCTargets = new GameObject[4];

	private bool buttonPressDelay = false;
	private float startButtonPressDelay; //Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		pRCTargets [0] = playerRCTarget1;
		pRCTargets [1] = playerRCTarget2;
		pRCTargets [2] = playerRCTarget3;
		pRCTargets [3] = playerRCTarget4;
		followMode = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
		}

		if (controller.RightStickButton && buttonPressDelay == false) {
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
			followMode = false;
		} 

		if (followMode == false) {
			Vector3 newPosition = new Vector3 (transform.position.x, 6f, transform.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, 4 * Time.deltaTime);
			if (controlsDisabled == false) {
				checkMovement ();
			}
			if (controller.Action4) {
				followMode = true;
			}
		} else {
			checkFollowDistance ();
		}

	}

	void FixedUpdate(){

		RaycastHit hit;
		for (int loop = 0; loop < 4; loop++) {
			lightDetectionRays [loop] = new Ray (transform.position, pRCTargets [loop].transform.position - transform.position);
			Debug.DrawLine (transform.position,  pRCTargets [loop].transform.position, Color.red);

			if (Physics.Raycast(lightDetectionRays [loop], out hit, 15f) && (hit.transform.gameObject.tag == "PRCTarget")) {
				Debug.DrawLine(hit.point, hit.point + Vector3.up*2f, Color.green);
				//Debug.Log ("Check");
			}
		}
	}

	void DisableControls(){
		controlsDisabled = true;
		Debug.Log ("Supp controls disabled");
	}
	void EnableControls(){
		controlsDisabled = false;
		Debug.Log ("Supp controls enabled");
	}

	void checkMovement(){
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;

		//Invert Controsl for when camera flips
		if (controlsInverted) {
			xInput *= -1f;
			zInput *= -1f;
		}
		Vector3 Movement = Vector3.zero;
		Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * moveSpeed;
		
		this.transform.Translate(Movement, Space.World); 
	}

	void SpotlightMode(){

	}

	void checkFollowDistance(){

		if (Mathf.Abs (this.transform.position.x - playerT.position.x) > xPosMaxOffset) {
			Vector3 newPosition = new Vector3 (playerT.position.x, transform.position.y, transform.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}

		if (Mathf.Abs (this.transform.position.y - playerT.position.y) != yPosMaxOffset) {
			Vector3 newPosition = new Vector3 (transform.position.x, playerT.position.y + yPosMaxOffset, transform.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}

		if (Mathf.Abs (this.transform.position.z - playerT.position.z) > zPosMaxOffset) {
			Vector3 newPosition = new Vector3 (transform.position.x, transform.position.y, playerT.position.z);
			this.transform.position = Vector3.Slerp (transform.position, newPosition, suppCharFollowSpeed * Time.deltaTime);
		}
	}
}

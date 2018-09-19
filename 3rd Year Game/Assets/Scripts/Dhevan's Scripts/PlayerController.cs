using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerController : MonoBehaviour {

	private InputDevice controller;

	public float moveSpeed = 10f;
	public float dashSpeedFactor = 30f;
	public float crouchSpeed = 5f;

	private bool dashing = false;
	private float startDashingTime;
	private bool crouching = false;
	private float startCrouchSquishTime;
	private float squishDuration = 0.25f;

	private float xInput, yInput, zInput;
	private bool controlsInverted = false;
	private bool controlsDisabled = false;

	public float jumpStrength = 4f;
	public float fallStrengthFactor = 4f;
	private bool jumped = false;
	public float jumpHeight = 1.5f;
	private Vector3 jumpPos;

	private bool buttonPressDelay = false;
	private float startButtonPressDelay; //Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	private Rigidbody playerRB;

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		playerRB = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		if (controlsDisabled == false) {
			checkMovement ();
		}
			checkActionButtons ();
			checkTriggerButtons ();
			checkStickButtons ();
			playerRB.rotation = Quaternion.identity; //prevent player object from rotating

			if (Time.time >= startDashingTime + 1f && dashing == true) {
				dashing = false;
				playerRB.velocity = Vector3.zero;
			}

			if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
				buttonPressDelay = false;
			}

			if (crouching == true) {
				RescaleSize (new Vector3 (2.1f, 0.75f, 2.1f));
			} else {
				RescaleSize (new Vector3 (1.5f, 1.5f, 1.5f));
			}
			
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
		if (crouching == false) {
			 Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * moveSpeed;
		} else if(crouching == true) {
			 Movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * crouchSpeed;
		}
		this.transform.Translate(Movement, Space.World); 
	}

	void checkActionButtons(){
		//Check Jump Action
		if ((controller.Action1 || Input.GetKey (KeyCode.Space)) && jumped == false) {
			playerRB.AddForce (new Vector3 (0f, jumpStrength * 100f, 0f), ForceMode.Force);
			jumped = true;
			jumpPos = this.transform.position;
		} 
		//Add a fall force after the jump
		else if(jumped == true) {
			if (this.transform.position.y - jumpPos.y >= jumpHeight) {
				playerRB.AddForce (new Vector3 (0f, -1f * (jumpStrength*50f/fallStrengthFactor), 0f), ForceMode.Force);
			}
		}
			
	}
	void checkTriggerButtons(){
		if (controller.RightTrigger == true && dashing == false) {
			playerRB.AddForce (new Vector3 (xInput, 0f, zInput) * dashSpeedFactor, ForceMode.Force);
			dashing = true;
			startDashingTime = Time.time;
			Debug.Log ("Dashed");
		}
	}
	void checkStickButtons(){
		if (controller.LeftStickButton == true && buttonPressDelay == false) {
			if (crouching == false) {
				startCrouchSquishTime = Time.time;
				crouching = true;
			} else if (crouching == true){
				crouching = false;
				startCrouchSquishTime = Time.time;
			}
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
		} 

		if (controller.RightStickButton == true && buttonPressDelay == false) {
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
			disableControls ();
		}
	}

	void InvertControls (){
		if (controlsInverted == true) {
			controlsInverted = false;
		} else {
			controlsInverted = true;
		}
	}

	void disableControls(){
		if (controlsDisabled == true) {
			controlsDisabled = false;
		} else {
			controlsDisabled = true;
		}
	}

	void RescaleSize(Vector3 newScale){

		float t = (Time.time - startCrouchSquishTime) / squishDuration;

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);
	}

	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Platform") {
			jumped = false;
		}
	}
}
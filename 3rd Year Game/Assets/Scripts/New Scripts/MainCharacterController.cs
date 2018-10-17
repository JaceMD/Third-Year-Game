using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

[RequireComponent(typeof(Rigidbody))]

public class MainCharacterController : MonoBehaviour {


	private Rigidbody playerRB;

	private InputDevice controller;

	private float xInput, yInput, zInput;

	public float moveSpeed = 6f;
	public float crawlSpeed = 3f;
	private float actualSpeed;

	public float jumpForce = 3.5f;
	public float fallMultiplier = 2.5f;
	public bool canJump = false;

	public bool isGrounded = false;

	private bool crawling = false;
	private float startCrawlingSquishTime;
	private float squishDuration = 0.25f;

	public float rotationSpeed = 10f;

	private bool buttonPressDelay = false;
	private float startButtonPressDelay;
	//Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		actualSpeed = moveSpeed;
		playerRB = this.GetComponent<Rigidbody> ();
		crawling = false;
	}
	
	// Update is called once per frame
	void Update () {
		checkOnGround ();
		checkCrawling ();
		checkRotation ();

		if (Time.time >= startButtonPressDelay + 0.35f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
		}
	}

	void FixedUpdate(){

		checkMovement ();
		checkJump ();
		checkFalling ();

	}

////////////////////////////////////////////////////////////////Primary Functions//////////////////////////////////////////////////////////////

	void checkMovement () //In Fixed Update
	{
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;


		Vector3 movement = Vector3.zero;
		if (crawling == false) {
			movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * actualSpeed;
		} else if (crawling == true) {
			movement = new Vector3 (xInput, 0f, zInput) * Time.deltaTime * crawlSpeed;
		}
		playerRB.MovePosition (transform.position + movement);
	}

	void checkJump(){ //In Fixed Update
		//Check Jump Action
		if ((controller.Action1 || Input.GetKey (KeyCode.Space)) && canJump == true && buttonPressDelay == false) {
			playerRB.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
			canJump = false;

			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
		} 

	}

	void checkFalling(){
		//check Falling
		if (playerRB.velocity.y < 0f) {
			playerRB.useGravity = false;
			Vector3 gravityForce = Vector3.up * -9.81f * (fallMultiplier);
			playerRB.AddForce (gravityForce, ForceMode.Acceleration);

		} else if (playerRB.velocity.y > 0f) {
			playerRB.useGravity = true;
		} else if (playerRB.velocity.y == 0f) {
			playerRB.AddForce (Vector3.up * (-1f)*jumpForce, ForceMode.Impulse);
			playerRB.useGravity = true;
		}
	}

	void checkRotation(){
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;

		if (Mathf.Abs (xInput) > 0f || Mathf.Abs (zInput) > 0f) {

			Vector3 dir = new Vector3 ((-1f) * xInput, 0f, (-1f) * zInput);

			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (dir), Time.deltaTime * rotationSpeed);
		}
	}



	void checkCrawling(){ //In Update
		if (controller.LeftStickButton == true && buttonPressDelay == false) {
			if (crawling == false) {
				startCrawlingSquishTime = Time.time;
				crawling = true;

			} else if (crawling == true) {
				crawling = false;
				startCrawlingSquishTime = Time.time;

			}
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
		} 

		if (crawling == true) {
			ScaleSize (new Vector3 (2.1f, 0.75f, 2.1f));
		} else {
			ScaleSize (new Vector3 (1.5f, 1.5f, 1.5f));
		}
	}

////////////////////////////////////////////////////////////////Support Functions//////////////////////////////////////////////////////////////

	void checkOnGround(){

		if (isGrounded == true) {
			canJump = true;
		} else {
			canJump = false;
		}
	}

	public void SetGrounded(bool b){
		isGrounded = b;
	}

	void ScaleSize (Vector3 newScale)
	{

		float t = (Time.time - startCrawlingSquishTime) / squishDuration;

		this.transform.localScale = Vector3.Lerp (this.transform.localScale, newScale, t);
	}

	public float toDegrees (float radians)
	{
		return radians * (180 / Mathf.PI);
	}
		


}

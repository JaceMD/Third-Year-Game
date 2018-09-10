using System.Collections.Generic;
using UnityEngine;
using InControl;

public class CharacterController : MonoBehaviour {

	private InputDevice controller;

	public float moveSpeed = 10f;
	public float dashSpeed = 30f;
	public float crouchSpeed = 5f;

	private float xInput, yInput, zInput;

	public float jumpStrength = 4f;
	public float fallStrengthFactor = 4f;
	private bool jumped = false;
	public float jumpHeight = 1.5f;
	private Vector3 jumpPos;

	private Rigidbody playerRB;

	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		playerRB = this.GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update () {
		checkMovement ();
		checkActionButtons ();
		playerRB.rotation = Quaternion.identity; //prevent player object from rotating
	}

	void checkMovement(){
		xInput = controller.LeftStick.X;  
		zInput = controller.LeftStick.Y;

		Vector3 Movement = new Vector3(xInput, 0f, zInput) * Time.deltaTime * moveSpeed;
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
		
	void OnCollisionEnter(Collision other){
		if (other.gameObject.tag == "Platform") {
			jumped = false;
		}
	}
}
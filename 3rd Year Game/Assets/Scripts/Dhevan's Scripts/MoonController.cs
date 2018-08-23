using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class MoonController : MonoBehaviour {

	private InputDevice controller;

	private bool moonRotationEnabled = false;
	public Transform moonTransform;
	public float moonRotationSpeed = 5f;

	private bool controlsInverted = false;
	public GameObject moonObj;
	public GameObject playerObj;
	public GameObject playerRCTarget1, playerRCTarget2, playerRCTarget3, playerRCTarget4;

	public Ray[] lightDetectionRays = new Ray[4];


	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
		if (controller.LeftTrigger) {
			//moonRotationEnabled = true;
			MoonRotationControls();

		} else {
			//moonRotationEnabled = false;
		}
			
		this.transform.position = new Vector3 (playerObj.transform.position.x, this.transform.position.y, playerObj.transform.position.z);
	}

	void FixedUpdate(){
		//lightDetectionRays [0] = new Ray (moonObj.transform.position, playerRCTarget1.transform.position);
		//lightDetectionRays [1] = new Ray (moonObj.transform.position, playerRCTarget2.transform.position);
		//lightDetectionRays [2] = new Ray (moonObj.transform.position, playerRCTarget3.transform.position);
		//lightDetectionRays [3] = new Ray (moonObj.transform.position, playerRCTarget4.transform.position);
		Ray ray = new Ray (moonObj.transform.position, playerRCTarget1.transform.position);

		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 20f)) {
			Debug.DrawLine(hit.point, hit.point + Vector3.up*5f, Color.green);
			Debug.Log ("Check");
		}
	

		Debug.DrawLine (moonObj.transform.position, playerRCTarget1.transform.position, Color.red);
		Debug.DrawLine (moonObj.transform.position, playerRCTarget2.transform.position, Color.red);
		Debug.DrawLine (moonObj.transform.position, playerRCTarget3.transform.position, Color.red);
		Debug.DrawLine (moonObj.transform.position, playerRCTarget4.transform.position, Color.red);
	}

	void MoonRotationControls(){
		float xInput = controller.RightStick.X;
		if (controlsInverted == false) {
			if (xInput > 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y - moonRotationSpeed, 0f);
			} else if (xInput < 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y + moonRotationSpeed, 0f);
			}
		} else {
			if (xInput > 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y + moonRotationSpeed, 0f);
			} else if (xInput < 0f) {
				moonTransform.eulerAngles = new Vector3 (0f, moonTransform.eulerAngles.y - moonRotationSpeed, 0f);
			}
		}
	}

	void InvertControls (){
		if (controlsInverted == true) {
			controlsInverted = false;
		} else {
			controlsInverted = true;
		}
	}
}

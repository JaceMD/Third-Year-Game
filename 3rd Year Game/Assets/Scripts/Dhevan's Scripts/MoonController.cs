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

	private Ray[] lightDetectionRays = new Ray[4];
	private GameObject[] pRCTargets = new GameObject[4];



	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
		pRCTargets [0] = playerRCTarget1;
		pRCTargets [1] = playerRCTarget2;
		pRCTargets [2] = playerRCTarget3;
		pRCTargets [3] = playerRCTarget4;
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


		RaycastHit hit;
		for (int loop = 0; loop < 4; loop++) {
			lightDetectionRays [loop] = new Ray (moonObj.transform.position, pRCTargets [loop].transform.position - moonObj.transform.position);
			Debug.DrawLine (moonObj.transform.position,  pRCTargets [loop].transform.position, Color.red);

			if (Physics.Raycast(lightDetectionRays [loop], out hit, 20f) && (hit.transform.gameObject.tag == "PRCTarget")) {
				Debug.DrawLine(hit.point, hit.point + Vector3.up*2f, Color.green);
				//Debug.Log ("Check");
			}
		}

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

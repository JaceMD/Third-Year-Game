using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyController : MonoBehaviour {

	private GameObject playerObj;
	public GameObject playerRCTarget1, playerRCTarget2, playerRCTarget3, playerRCTarget4;

	private Ray[] lightDetectionRays = new Ray[4];
	private GameObject[] pRCTargets = new GameObject[4];

	// Use this for initialization
	void Start () {
		playerObj = GameObject.Find ("Player");
		pRCTargets [0] = playerRCTarget1;
		pRCTargets [1] = playerRCTarget2;
		pRCTargets [2] = playerRCTarget3;
		pRCTargets [3] = playerRCTarget4;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void FixedUpdate(){

		RaycastHit hit;
		for (int loop = 0; loop < 4; loop++) {
			lightDetectionRays [loop] = new Ray (transform.position, pRCTargets [loop].transform.position - transform.position);
			Debug.DrawLine (transform.position,  pRCTargets [loop].transform.position, Color.red);

			if (Physics.Raycast(lightDetectionRays [loop], out hit, 5f) && (hit.transform.gameObject.tag == "PRCTarget")) {
				Debug.DrawLine(hit.point, hit.point + Vector3.up*2f, Color.green);
				//Debug.Log ("Check");
			}
		}
	}
}

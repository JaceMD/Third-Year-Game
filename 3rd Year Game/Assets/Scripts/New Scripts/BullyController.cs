using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyController : MonoBehaviour {

	public float detectionRadius = 5f;

	private float playerDistance;
	private GameObject player;
	private Transform playerT;

	private Ray visualDetectionRay;
	private GameObject centrePRCTarget;

	public float alertTime = 1f;
	private float currentAlertTime;


	private StealthManager playerSM; 

	[SerializeField]
	private bool alerted = false;
	[SerializeField]
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		playerT = player.transform;
		playerSM = player.GetComponent<StealthManager> ();
		centrePRCTarget = GameObject.Find ("PlayerRayCastTarget9");
		currentAlertTime = alertTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (alerted == true) {
			currentAlertTime -= Time.deltaTime;
			if (currentAlertTime <= 0f) {
				gameOver = true;

				//code to disable player controller and start game over sequence.
			}
		} 
		else if(alerted == false){
			//continue surveying
			currentAlertTime = alertTime;
		}
	}

	void FixedUpdate(){
		checkPlayerDistance ();
	}

	void checkPlayerDistance(){
		playerDistance = Vector3.Magnitude (playerT.position - this.transform.position);
		Debug.Log ("Player Distance: " + playerDistance);
		if (playerDistance <= detectionRadius) {
			
			//CheckIfPlayerInLight
			RaycastHit hit;

			visualDetectionRay = new Ray (transform.position, centrePRCTarget.transform.position - transform.position);
			Debug.DrawLine (transform.position, centrePRCTarget.transform.position, Color.red);

			if (Physics.Raycast (visualDetectionRay, out hit, detectionRadius) && ((hit.transform.gameObject.tag == "PRCTarget") || hit.transform.gameObject.tag == "Player")) {
				alerted = true;
				Debug.DrawLine (hit.point, hit.point + Vector3.up * 2f, Color.green);
			} else {
				alerted = false;
			}

			//If Player Is In Light - Get Alerted - After alertTime seconds chase player and game over
			//else continue surveying area.
		} else {
			alerted = false;
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, detectionRadius);
	}
}

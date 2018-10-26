using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullyController : MonoBehaviour {

	public float detectionRadius = 5f;

	private float playerDistance;
	private Transform playerT;

	public float alertTime = 1f;
	private bool alerted = false;
	private bool gameOver = false;

	// Use this for initialization
	void Start () {
		playerT = GameManager.instance.player.transform;
	}
	
	// Update is called once per frame
	void Update () {
		checkPlayerDistance ();
	}

	void checkPlayerDistance(){
		playerDistance = Vector3.Magnitude (playerT.position - this.transform.position);

		if (playerDistance <= detectionRadius) {
			
			//CheckIfPlayerInLight

			//If Player Is In Light - Get Alerted - After alertTime seconds chase player and game over
			//else continue surveying area.
		}
	}

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, detectionRadius);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartLevelController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "Player") {
			GameObject.Find ("SceneController").GetComponent<SceneController> ().RestartLevel ();
		}
	}
}

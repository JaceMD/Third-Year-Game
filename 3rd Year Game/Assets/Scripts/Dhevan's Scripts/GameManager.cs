using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class GameManager : MonoBehaviour {

	private InputDevice controller;
	private bool buttonPressDelay = false;
	private float startButtonPressDelay; //Since Incontrol doesnt have a getDown method, this is used to check for single button presses.

	private bool cameraOnPlayer = true;


	// Use this for initialization
	void Start () {
		controller = InputManager.ActiveDevice;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if (controller.RightStickButton && buttonPressDelay == false) {
			if (cameraOnPlayer == true) {
				cameraOnPlayer = false;
				GameObject.Find ("Player").SendMessage ("DisableControls");
				GameObject.Find ("Support Character").SendMessage ("EnableControls");
			} else {
				cameraOnPlayer = true;
				GameObject.Find ("Player").SendMessage ("EnableControls");
				GameObject.Find ("Support Character").SendMessage ("DisableControls");
			}
			buttonPressDelay = true;
			startButtonPressDelay = Time.time;
		}
		*/

		if (Time.time >= startButtonPressDelay + 0.5f && buttonPressDelay == true) { //User has to wait 0.5 seconds to repress a button
			buttonPressDelay = false;
		}
	}




}

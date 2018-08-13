using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravityController : MonoBehaviour {

    public Rigidbody PlayerRB;
    public bool Grounded;
    public CameraController LandingScreenShakeTimer;

    // Use this for initialization
    void Start () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            LandingScreenShakeTimer.LandingShakeTimer = 1;
            LandingScreenShakeTimer.CameraOriginalPos = LandingScreenShakeTimer.CameraTransform.position;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            PlayerRB.useGravity = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            PlayerRB.useGravity = true;
        }
    }

    // Update is called once per frame
    void Update () {
		if(PlayerRB.useGravity == true)
        {
            Grounded = false;
        }
        else
        {
            Grounded = true;
        }
	}
}

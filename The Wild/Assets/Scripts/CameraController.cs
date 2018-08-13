using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public Transform CameraTransform;
    public Transform PlayerTransform;
    public float Speed;

    public float LandingShakeTimer;
    public float DecreaseFactor;
    public float ShakeAmount;
    public Vector3 CameraOriginalPos;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (LandingShakeTimer > 0)
        {
            CameraTransform.position = new Vector3(CameraTransform.position.x, CameraOriginalPos.y + (Random.Range(-1,1) * ShakeAmount), CameraTransform.position.z);
            LandingShakeTimer -= DecreaseFactor * Time.deltaTime;
        }
        else
        {
            LandingShakeTimer = 0;
            CameraTransform.position = Vector3.MoveTowards(CameraTransform.position, new Vector3(PlayerTransform.position.x, 10, -5), Speed * Time.deltaTime);
        }
    }
}

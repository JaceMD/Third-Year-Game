using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementControler : MonoBehaviour {

    public Rigidbody EnemyRB;
    public float OriginalSpeed;
    public float Speed;
    public float OriginalMaxVelocity;
    public float MaxVelocity;
    public float Timer;
    private float RightTimer;
    private float LeftTimer;

    // Use this for initialization
    void Start ()
    {
        RightTimer = Timer;
	}
	
	// Update is called once per frame
	void Update () {
		if(RightTimer > 0)
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            RightTimer = RightTimer - Time.deltaTime;
            EnemyRB.AddForce(transform.forward * Speed);
            if(RightTimer <= 0)
            {
                EnemyRB.velocity = new Vector3(0, 0, 0);
                LeftTimer = Timer;
            }
        }
        if (LeftTimer > 0)
        {
            transform.eulerAngles = new Vector3(0, -90, 0);
            LeftTimer = LeftTimer - Time.deltaTime;
            EnemyRB.AddForce(transform.forward * Speed);
            if (LeftTimer <= 0)
            {
                EnemyRB.velocity = new Vector3(0, 0, 0);
                RightTimer = Timer;
            }
        }


    }
}

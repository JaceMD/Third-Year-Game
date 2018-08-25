using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public Transform[] Waypoints;
    private int nextPoint;
    public float Speed;
    public float setTimer;
    private float waitTimer;
    private bool touched;

	// Use this for initialization
	void Start () {
        nextPoint = 1;
        transform.LookAt(Waypoints[nextPoint].position);
        waitTimer = 0;
    }

    // Update is called once per frame
    void Update () {
        if(waitTimer >= 0)
        {
            waitTimer -= Time.deltaTime;
        }
		if(transform.position == Waypoints[nextPoint].position)
        {
            if(touched == false)
            {
                waitTimer = setTimer;
                touched = true;
            }
            if (waitTimer <= 0)
            {
                touched = false;

                if (nextPoint == Waypoints.Length - 1)
                {
                    nextPoint = 0;
                    transform.LookAt(Waypoints[nextPoint].position);
                }
                else
                {
                    nextPoint += 1;
                    transform.LookAt(Waypoints[nextPoint].position);
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, Waypoints[nextPoint].position, Speed * Time.deltaTime);
	}
}

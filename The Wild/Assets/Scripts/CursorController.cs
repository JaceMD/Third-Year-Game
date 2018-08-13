using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour {

    public Transform Player;
    public float Speed;

	// Use this for initialization
	void Start () {
        this.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
        if (this.transform.position.x > Player.position.x - 5)
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                this.transform.position = new Vector3(transform.position.x - Speed, transform.position.y, transform.position.z);
            }
        }
        if (this.transform.position.x < Player.position.x + 5)
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                this.transform.position = new Vector3(transform.position.x + Speed, transform.position.y, transform.position.z);
            }
        }
        if (this.transform.position.z > -1)
        {
            if (Input.GetAxis("Mouse Y") < 0)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - Speed);
            }
        }
        if (this.transform.position.z < -1)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, -0.9f);
        }
        if (this.transform.position.z < 1)
        {
            if (Input.GetAxis("Mouse Y") > 0)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + Speed);
            }
        }
        if (this.transform.position.z > 1)
        {
            this.transform.position = new Vector3(transform.position.x, transform.position.y, 0.9f);
        }
        if (this.transform.position.y > 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y - Speed *2, transform.position.z);
            }
        }
        if (this.transform.position.y < 1f)
        {
            this.transform.position = new Vector3(transform.position.x, 1.1f, transform.position.z);
        }
        if (this.transform.position.y < 8)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                this.transform.position = new Vector3(transform.position.x, transform.position.y + Speed *2, transform.position.z);
            }
        }

        /*Vector3 temp = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        /*if (temp.z > 6f)
        {
            temp.z = 6f;
        }
        if (temp.z < 5)
        {
            temp.z = 5;
        }
        else
        {
            temp.z = Input.mousePosition.z;
        }*/
        //this.transform.position = temp;
    }
}

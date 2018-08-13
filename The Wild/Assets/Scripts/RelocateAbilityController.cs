using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelocateAbilityController : MonoBehaviour {

    public GameObject CursorActive;
    public Transform Cursor;
    public PlayerMovementController Player;

	// Use this for initialization
	void Start () {
        Player = GetComponent<PlayerMovementController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (CursorActive.active == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                this.transform.position = Cursor.position;
            }
        }
	}
}

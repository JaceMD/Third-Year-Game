using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineBetweenObectsController : MonoBehaviour {

    public GameObject Player;
    public GameObject Cursor;
    private LineRenderer line;

	// Use this for initialization
	void Start () {
        line = this.gameObject.AddComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.positionCount = 2;
	}
	
	// Update is called once per frame
	void Update () {
		if(Player != null && Cursor != null)
        {
            line.SetPosition(0, Player.transform.position);
            line.SetPosition(1, Cursor.transform.position);
        }
	}
}

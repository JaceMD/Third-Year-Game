using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoxController : MonoBehaviour {

    public GameManager resetPos;

	// Use this for initialization
	void Start () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.transform.position = resetPos.CheckpointPos;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

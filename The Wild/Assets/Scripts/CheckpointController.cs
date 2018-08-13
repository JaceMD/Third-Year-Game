using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour {

    public GameManager setCheckpos;

	// Use this for initialization
	void Start () {
        setCheckpos.CheckpointPos = new Vector3(-9.25f, 1.1f, 0);
	}

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            setCheckpos.CheckpointPos = other.transform.position;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

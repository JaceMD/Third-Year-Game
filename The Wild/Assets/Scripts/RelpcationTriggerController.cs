using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelpcationTriggerController : MonoBehaviour {

    public GameObject Cursor;

	// Use this for initialization
	void Start () {
		
	}

    void OnCollisionStay(Collision other)
    {
        if(other.gameObject.tag == "Player")
        {
            Cursor.SetActive(true);
        }
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Cursor.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

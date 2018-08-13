using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallViewTriggerController : MonoBehaviour {

    public GameObject PlayerShadow;

	// Use this for initialization
	void Start ()
    {
        PlayerShadow.SetActive(false);

    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerShadow.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerShadow.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

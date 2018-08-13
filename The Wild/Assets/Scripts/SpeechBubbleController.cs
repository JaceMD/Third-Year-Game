using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechBubbleController : MonoBehaviour {

    public Text[] Speech;

	// Use this for initialization
	void Start () {
		for(int i = 0; i <= 13; i++)
        {
            Speech[i].enabled = false;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            this.transform.localScale = new Vector3(1, 2, 1);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechTriggerController : MonoBehaviour {

    public SpeechBubbleController SpeechBubble;
    public int TriggerNumber;
    public GameObject SpeechBubbleObject;

	// Use this for initialization
	void Start () {
        SpeechBubbleObject.SetActive(false);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(TriggerNumber == 10 || TriggerNumber == 11)
            {
                //SpeechBubbleObject.transform.localPosition = new Vector3(-3.1f, -0.4f, -3);
            }
            SpeechBubble.Speech[TriggerNumber].enabled = true;
            SpeechBubbleObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (TriggerNumber == 10 || TriggerNumber == 11)
            {
                SpeechBubbleObject.transform.localPosition = new Vector3(-3.1f, 5, -3);
            }
            SpeechBubble.Speech[TriggerNumber].enabled = false;
            SpeechBubbleObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}

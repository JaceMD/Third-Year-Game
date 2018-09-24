using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetectionController : MonoBehaviour {

	public bool[] pRCTargetsVisible = new bool[9];
	public Image detectionUIImage;

	private float alphaPercentage;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 9; i++) {
			pRCTargetsVisible [i] = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		updateAlphaUI ();
	}

	public void setPRCTargetVisible(int elem, bool visibility){
		pRCTargetsVisible [elem] = visibility;
	}

	void updateAlphaUI(){
		float alpha = checkVisibilityAlphaRatio ();
		float actualAlpha = detectionUIImage.color.a;

		//Fade Alpha over time smoothly
		if (actualAlpha > alpha) {
			actualAlpha -= Time.deltaTime;
		} else if (actualAlpha < alpha) {
			actualAlpha += Time.deltaTime;
		} else {
			actualAlpha = alpha;
		}

		detectionUIImage.color = new Color(detectionUIImage.color.r, detectionUIImage.color.g, detectionUIImage.color.b, actualAlpha);

		//check distance alpha
	}
	float checkVisibilityAlphaRatio(){
		int numPRCTargetsVisible = 0;
		float alphaRatio;

		for (int i = 0; i < 9; i++) {
			if (pRCTargetsVisible [i] == true) {
				numPRCTargetsVisible++;
			}
		}
		alphaRatio = numPRCTargetsVisible / 9f;
		return alphaRatio;

	}
	float checkDistanceAlpha(float distance){
		return 0f;
	}
	void playerDetected(){
		detectionUIImage.color = new Color(1f, 0f, 0f, 1f);
	}




}

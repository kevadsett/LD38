using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Renderer))]
public class CuteWeeFace : MonoBehaviour {
	[SerializeField] MaterialSelection matSelection;
	[SerializeField] MaterialSelection blinkSelection;
	[SerializeField] int defaultFace;
	[SerializeField] float minBlink;
	[SerializeField] float maxBlink;
	[SerializeField] float blinkDuration;

	Renderer target;
	float blinkTimer;
	int matID;

	public void MakeHappy () {
		matID = 1;
	}

	public void MakeSad () {
		matID = 0;
	}

	void Awake () {
		matID = defaultFace;
		target = GetComponent<Renderer> ();
		blinkTimer = Random.Range (0f, maxBlink);
	}

	void Update () {
		if (blinkTimer < 0f) {
			blinkTimer = Random.Range (minBlink, maxBlink);
		}

		blinkTimer -= Time.deltaTime;

		if (blinkTimer < blinkDuration) {
			target.sharedMaterial = blinkSelection.GetIndex (matID);
		} else {
			target.sharedMaterial = matSelection.GetIndex (matID);
		}
	}
}

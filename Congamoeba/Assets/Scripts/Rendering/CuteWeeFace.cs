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
	[SerializeField] float speakDuration;

	Renderer target;
	float blinkTimer;
	float speechTimer;
	int matID;

	public void MakeHappy () {
		matID = 1;
	}

	public void MakeSad () {
		matID = 0;
	}

	public void SayWord () {
		speechTimer = speakDuration;
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

		int idToUse = (speechTimer > 0f) ? 2 : matID;


		if (blinkTimer < blinkDuration) {
			target.sharedMaterial = blinkSelection.GetIndex (idToUse);
		} else {
			target.sharedMaterial = matSelection.GetIndex (idToUse);
		}

		blinkTimer -= Time.deltaTime;
		speechTimer -= Time.deltaTime;
	}
}

using UnityEngine;
using System.Collections;

public class AmoebaFace : MonoBehaviour {
	[SerializeField] Transform face;
	[SerializeField] Transform stretcher;
	[SerializeField] Transform stretchee;
	[SerializeField] float effectScale;
	[SerializeField] float scaleMin;
	[SerializeField] float scaleMax;
	[SerializeField] bool doDebugStuff;

	// debug editor stuff
	void Update () {
		if (doDebugStuff == false) return;

		UpdateDirection (new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical")));
	}


	public void UpdateDirection (Vector3 velocity) {
		var effectVector = velocity * effectScale;
		if (effectVector.sqrMagnitude > 1f) {
			effectVector.Normalize();
		}

		face.localPosition = effectVector * 0.5f;

		stretcher.right = effectVector.normalized;
		stretchee.rotation = Quaternion.identity;

		float x = Mathf.Lerp (1f, scaleMax, effectVector.magnitude);
		float y = Mathf.Lerp (1f, scaleMin, effectVector.magnitude);

		stretcher.localScale = new Vector3 (x, y, 1f);
	}
}
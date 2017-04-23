using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (MaskableGraphic))]
public class ImageEnabledAxis : MonoBehaviour {
	[SerializeField] string axis;
	[SerializeField] float duration;
	[SerializeField] bool reverse;
	[SerializeField] bool invert;

	MaskableGraphic graphic;
	Color color;

	void Awake () {
		graphic = GetComponent<MaskableGraphic> ();
		color = graphic.color;
	}

	void Update () {
		float amt = Input.GetAxis (axis);

		if (reverse) {
			amt = Mathf.Clamp01 (0f - amt);
		} else {
			amt = Mathf.Clamp01 (amt);
		}

		if (invert) {
			color.a = 1f - amt;
		} else {
			color.a = amt;
		}
		graphic.color = color;
	}
}
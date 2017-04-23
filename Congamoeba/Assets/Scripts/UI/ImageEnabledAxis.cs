using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (MaskableGraphic))]
public class ImageEnabledAxis : MonoBehaviour {
	[SerializeField] string axis;
	[SerializeField] float duration;
	[SerializeField] bool invert;

	MaskableGraphic graphic;
	Color color;
	float time;

	void Awake () {
		graphic = GetComponent<MaskableGraphic> ();
		color = graphic.color;
	}

	void Update () {
		time -= Time.deltaTime / duration;

		if (Input.GetAxis (axis) > 0f && !invert) {
			time = 1f;
		}

		if (Input.GetAxis (axis) < 0f && invert) {
			time = 1f;
		}

		color.a = Mathf.Clamp01 (time);
		graphic.color = color;
	}
}

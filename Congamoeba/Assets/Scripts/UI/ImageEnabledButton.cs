using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (MaskableGraphic))]
public class ImageEnabledButton : MonoBehaviour {
	[SerializeField] string button;
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
		if (ButtonUnlocker.IsButtonUnlocked (button)) {
			time -= Time.deltaTime / duration;

			if (Input.GetButton (button) ^ invert) {
				time = 1f;
			}

			color.a = Mathf.Clamp01 (time);
		} else {
			color.a = 0f;
		}

		graphic.color = color;
	}
}

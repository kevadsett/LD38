using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (MaskableGraphic))]
public class ImageEnabledButton : MonoBehaviour {
	[SerializeField] string button;
	[SerializeField] float duration;

	MaskableGraphic graphic;
	Color color;
	float time;

	void Awake () {
		graphic = GetComponent<MaskableGraphic> ();
		color = graphic.color;
	}

	void Update () {
		time -= Time.deltaTime / duration;

		if (Input.GetButton (button)) {
			time = 1f;
		}

		color.a = Mathf.Clamp01 (time);
		graphic.color = color;
	}
}

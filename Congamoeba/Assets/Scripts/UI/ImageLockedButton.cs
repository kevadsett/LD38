using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (MaskableGraphic))]
public class ImageLockedButton : MonoBehaviour {
	[SerializeField] string button;

	MaskableGraphic graphic;

	void Awake () {
		graphic = GetComponent<MaskableGraphic> ();
	}

	void Update () {
		if (ButtonUnlocker.IsButtonUnlocked (button)) {
			graphic.enabled = false;
		}
	}
}

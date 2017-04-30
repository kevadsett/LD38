using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
[RequireComponent (typeof (RectTransform))]
public class SyllableIcon : MonoBehaviour {
	[SerializeField] int id;
	[SerializeField] float spacing;
	[SerializeField] float padding;
	[SerializeField] float fadeTime;
	[SerializeField] float notPlayingFade;

	static int numInUse = 0;
	static int numToShow = 0;
	static SyllableIcon[] icons;

	Image graphic;
	RectTransform rect;
	Color color;

	float timer = 0f;

	public static void RevealIcon (int index, int count, Sprite sprite) {
		numInUse = count;
		numToShow = index + 1;
		icons[index].timer = 1f;
		icons[index].graphic.sprite = sprite;
	}

	public static void ResetIcons () {
		numInUse = 0;
		numToShow = 0;
	}

	void Awake () {
		graphic = GetComponent<Image> ();
		rect = GetComponent<RectTransform> ();
		color = graphic.color;

		if (icons == null) {
			icons = new SyllableIcon[8];
		}

		icons[id] = this;
	}

	void Update () {
		timer -= Time.deltaTime / fadeTime;

		if (numToShow > id) {
			float leftMost = (padding * -0.5f * (numInUse - 1)) + (spacing * -0.5f * numInUse);
			float myPos = leftMost + spacing * id + padding * id;

			rect.anchoredPosition = new Vector3 (myPos, 0f);

			timer = Mathf.Max (timer, notPlayingFade);
		}

		color.a = timer;
		graphic.color = color;
	}
}
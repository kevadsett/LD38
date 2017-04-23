using UnityEngine;
using System.Collections;

[CreateAssetMenu]
public class MaterialSelection : ScriptableObject {
	[SerializeField] Material[] mats;

	public Material GetRandom () {
		return mats[Random.Range (0, mats.Length)];
	}
}
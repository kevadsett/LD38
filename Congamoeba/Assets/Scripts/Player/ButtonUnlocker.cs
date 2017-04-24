using UnityEngine;
using System.Collections.Generic;

public static class ButtonUnlocker {
	static List<string> states = new List<string> () {
		"",
	};

	public static void UnlockButton (string id) {
		if (states.Contains (id) == false) {
			states.Add (id);
		}
	}

	public static bool GetButtonDown (string id) {
		return Input.GetButtonDown (id) && IsButtonUnlocked (id);
	}

	public static bool IsButtonUnlocked (string id) {
		return states.Contains (id);
	}
}
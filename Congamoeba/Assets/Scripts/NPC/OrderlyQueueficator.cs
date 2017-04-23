using UnityEngine;
using System.Collections.Generic;

public static class OrderlyQueueficator {
	static List<Transform> theQueue = new List<Transform> ();

	public static void AddMe (Transform me) {
		theQueue.Add (me);
	}

	public static void RemoveMe (Transform me) {
		theQueue.Remove (me);
	}

	public static Transform GetMyTarget (Transform me) {
		int n = GetMyIndex (me) - 1;

		if (n < 0) {
			return null;
		}

		return theQueue[n];
	}

	public static Vector3 GetAveragePosition () {
		Vector3 pos = Vector3.zero;

		int count = theQueue.Count;
		for (int i = 0; i < count; i++) {
			pos += theQueue[i].position;
		}

		return pos * 1f / count;
	}

	static int GetMyIndex (Transform me) {
		return theQueue.FindIndex (b => (b == me));
	}
}
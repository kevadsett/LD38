using UnityEngine;
using System.Collections.Generic;
using Congamoeba.NPC;

public class NpcGeneration : MonoBehaviour {
	public GameObject NpcPrefab;

	public List<int> GenerationBands;

	public float DistanceBetweenBands;

	public float Randomisation;

	private int _totalNpcsToCreate;
	private int _totalCreatedNpcs;

	void Start ()
	{
		for (int i = 0; i < GenerationBands.Count; i++)
		{
			_totalNpcsToCreate += GenerationBands[i];
		}
	}

	void Update ()
	{
		if (_totalCreatedNpcs < _totalNpcsToCreate)
		{
			for (int i = 0; i < GenerationBands.Count; i++)
			{
				int npcCount = GenerationBands [i];
				float bandDistance = (i + 1) * DistanceBetweenBands;

				float step = ((Mathf.PI * 2f) / npcCount);

				float offset = Random.value * Mathf.PI * 2f;
				for (int j = 0; j < npcCount; j++)
				{
					float angle = offset + step * j;
					float x = bandDistance * Mathf.Cos (angle) + ((Random.value * 2) - 1) * Randomisation;
					float y = bandDistance * Mathf.Sin (angle) + ((Random.value * 2) - 1) * Randomisation;

					GameObject npc = GameObject.Instantiate (NpcPrefab);
					npc.GetComponent<NpcStateMachine> ().Difficulty = i;
					npc.transform.position = new Vector3 (x, y, npc.transform.position.z);
					_totalCreatedNpcs++;
				}
			}
		}
	}
}

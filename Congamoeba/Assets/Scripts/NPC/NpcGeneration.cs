using UnityEngine;
using System.Collections.Generic;
using Congamoeba.NPC;
using Congamoeba.Player;

public class NpcGeneration : MonoBehaviour {
	public GameObject NpcPrefab;

	public int NumberToGenerate;
	public float GenerationDistance;
	public float SpaceNeeded = 1f;

	public GameObject Player;

	private PlayerPhysics _playerPhysics;

	private static NpcGeneration _instance;

	private static List<NpcStateMachine> _npcs = new List<NpcStateMachine>();

	private const float SCALE_DAMPER = 0.5f;

	private static int _npcsGenerated;
	void Start ()
	{
		_instance = this;
		_playerPhysics = Player.GetComponent<PlayerPhysics> ();
	}

	void Update ()
	{
		if (_npcs.Count < NumberToGenerate)
		{
			Generate ();
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.green;
		float scale = Player.transform.localScale.x;
		Gizmos.DrawWireSphere(Player.transform.position, GenerationDistance * scale * SCALE_DAMPER - (5 * scale * SCALE_DAMPER));
		Gizmos.DrawWireSphere(Player.transform.position, GenerationDistance * scale * SCALE_DAMPER + (5 * scale * SCALE_DAMPER));
	}

	public void Generate()
	{
		float scale = Player.transform.localScale.x;
		float angle = 0;
		if (_playerPhysics.Acceleration.magnitude < 0.01f)
		{
			angle = Random.value * Mathf.PI * 2f;
		}
		else
		{
			angle = Mathf.Atan2 (_playerPhysics.Acceleration.y, _playerPhysics.Acceleration.x);
			angle += ((Random.value * Mathf.PI) - (Mathf.PI / 2f));
		}
		float distance = GenerationDistance * scale * SCALE_DAMPER + ((Random.value * 10 * scale * SCALE_DAMPER) - 5 * scale * SCALE_DAMPER);
		float x = Player.transform.position.x + distance * Mathf.Cos (angle);
		float y = Player.transform.position.y + distance * Mathf.Sin (angle);

		NpcStateMachine closestNpc;

		float nearestNpcDistance = GetNearestNpcDistance (new Vector3 (x, y, 0), out closestNpc);

		int attempt = 0;
		const int maxAttempts = 10;

		float direction = Random.value > 0.5 ? 1f : -1f;
		while (nearestNpcDistance < (SpaceNeeded * scale * SCALE_DAMPER) && attempt < maxAttempts)
		{
			float tenthOfPi = Mathf.PI * 0.1f;
			float angleChange = (Random.value * tenthOfPi);
			angle += angleChange * direction;
			distance = GenerationDistance * scale * SCALE_DAMPER + ((Random.value * 10 * scale * SCALE_DAMPER) - 5 * scale * SCALE_DAMPER);
			x = Player.transform.position.x + distance * Mathf.Cos (angle);
			y = Player.transform.position.y + distance * Mathf.Sin (angle);
			nearestNpcDistance = GetNearestNpcDistance (new Vector3 (x, y, 0), out closestNpc);
			attempt++;
		}
		if (attempt == maxAttempts)
		{
			return;
		}

		GameObject npc = GameObject.Instantiate (NpcPrefab);
		npc.name = "NPC(" + _npcsGenerated + ")";
		npc.transform.localScale = Player.transform.localScale;
		npc.transform.position = new Vector3 (x, y, Random.value);
		NpcStateMachine stateMachine = npc.GetComponent<NpcStateMachine> ();
		stateMachine.PlayerTransform = Player.transform;
		stateMachine.KillDistance = GenerationDistance * scale * SCALE_DAMPER + (5 * scale * SCALE_DAMPER);
		_npcs.Add (stateMachine);
		_npcsGenerated++;
	}

	private float GetNearestNpcDistance(Vector3 pos, out NpcStateMachine closestNpc)
	{
		Debug.Log (_npcs.Count);
		float closestMag = 9999f;
		closestNpc = null;
		foreach (NpcStateMachine npc in _npcs)
		{
			float distance = Mathf.Abs ((pos - npc.transform.position).magnitude);
			Debug.Log (distance);
			if (distance < closestMag)
			{
				closestMag = distance;
				closestNpc = npc;
			}
		}
		return closestMag;
	}

	public static void GenerateNewNpc()
	{
		Debug.Log ("Generating new npc");
		if (_instance != null)
		{
			_instance.Generate ();
		}
	}

	public static void Kill(NpcStateMachine npc)
	{
		Object.Destroy(npc.gameObject);
		_npcs.Remove(npc);
	}
}

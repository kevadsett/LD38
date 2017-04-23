using Congamoeba.GameStateMachine;
using UnityEngine;
using Congamoeba.Player;

namespace Congamoeba.NPC
{
	public class IdlingState : IGameState
	{
		public Camera StateCamera { get { return null; } }

		private GameObject _npc;

		private PlayerPhysics _npcPhysics;
//		private NpcStateMachine _stateMachine;

		private const float MINIMUM_DIRECTION_DURATION = 0f;

		private const float MAX_SPEED = 0.01f;

		public IdlingState (GameObject npc)
		{
			_npc = npc;
			_npcPhysics = _npc.GetComponent<PlayerPhysics> ();
			_npcPhysics.Acceleration = Random.insideUnitCircle * MAX_SPEED;
//			_stateMachine = npc.GetComponent<NpcStateMachine> ();
		}

		public void OnEnter()
		{
		}

		public void Update ()
		{
			float xChange = ((Random.value * 2) - 1) * MAX_SPEED / 10;
			float yChange = ((Random.value * 2) - 1) * MAX_SPEED / 10;
			_npcPhysics.Acceleration.x = Mathf.Clamp (_npcPhysics.Acceleration.x + xChange, -MAX_SPEED, MAX_SPEED);
			_npcPhysics.Acceleration.y = Mathf.Clamp (_npcPhysics.Acceleration.y + yChange, -MAX_SPEED, MAX_SPEED);
		}

		public void OnExit ()
		{
		}
	}
}


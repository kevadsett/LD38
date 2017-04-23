using UnityEngine;
using System.Collections;
using Congamoeba.GameStateMachine;

namespace Congamoeba.NPC
{
	public class FollowState : IGameState
	{
		public Camera StateCamera { get { return null; } }

		Transform self;
		Transform target;

		const float outerDist = 2f;
		const float innerDist = 1.5f;

		public FollowState (GameObject npc)
		{
			self = npc.transform;
			target = Congamoeba.Player.PlayerMovementController.PlayerTransform;
		}

		public void OnEnter()
		{
			
		}

		public void Update ()
		{
			Vector3 targetPos = target.position;
			Vector3 between = targetPos - self.position;

			if (between.magnitude > outerDist)
			{
				// move towards target
			}
			else if (between.magnitude < innerDist)
			{
				// move away from target
			}
		}

		public void OnExit ()
		{
		}
	}
}
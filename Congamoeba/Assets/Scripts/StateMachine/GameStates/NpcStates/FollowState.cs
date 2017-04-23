using UnityEngine;
using System.Collections;
using Congamoeba.GameStateMachine;
using Congamoeba.Player;
using System.Collections.Generic;

namespace Congamoeba.NPC {
	public class FollowState : IGameState {
		public Camera StateCamera { get { return null; } }

		Transform self;
		Transform player;
		PlayerPhysics physics;

		const float outerDist = 1.6f;
		const float innerDist = 1.3f;

		public FollowState (GameObject npc) {
			self = npc.transform;
			physics = npc.GetComponent<PlayerPhysics> ();

			player = PlayerMovementController.PlayerTransform;
		}

		public void OnEnter() {
			OrderlyQueueficator.AddMe (self);
		}

		public void Update () {
			Transform target = OrderlyQueueficator.GetMyTarget (self);

			Vector3 playerBetween = player.position - self.position;
			Vector3 accel = new Vector3 (0f, 0f, 0f);

			// prioritise avoiding the player, then following, then backing away
			if (playerBetween.magnitude < innerDist)
			{
				accel = new Vector3 (playerBetween.x, playerBetween.y).normalized * -1f;
			}
			else if (target != null) {
				Vector3 between = target.position - self.position;

				if (between.magnitude > outerDist)
				{
					accel = new Vector3 (between.x, between.y).normalized;
				}
				else if (between.magnitude < innerDist)
				{
					accel = new Vector3 (between.x, between.y).normalized * -1f;
				}
			}

			physics.Acceleration = accel;
		}

		public void OnExit () {
			OrderlyQueueficator.RemoveMe (self);
			physics.Stop ();
		}
	}
}
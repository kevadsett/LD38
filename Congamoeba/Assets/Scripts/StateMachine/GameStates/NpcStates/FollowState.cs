using UnityEngine;
using System.Collections;
using Congamoeba.GameStateMachine;
using Congamoeba.Player;
using System.Collections.Generic;

namespace Congamoeba.NPC {
	public class FollowState : IGameState {
		public Camera StateCamera { get { return null; } }

		Transform self;
		PlayerPhysics physics;
		CuteWeeFace face;

		const float outerDist = 1.6f;
		const float innerDist = 1.3f;

		public FollowState (GameObject npc) {
			self = npc.transform;
			physics = npc.GetComponent<PlayerPhysics> ();
			face = npc.GetComponentInChildren<CuteWeeFace> ();
		}

		public void OnEnter() {
			OrderlyQueueficator.AddMe (self);
			face.MakeHappy ();
		}

		public void Update () {
			Transform target = OrderlyQueueficator.GetMyTarget (self);
			Transform player = PlayerMovementController.PlayerTransform;
			Transform chatPartner = ConversationState.ChatPartnerTransform;

			Vector3 playerBetween = player.position - self.position;
			Vector3 accel = new Vector3 (0f, 0f, 0f);

			// prioritise avoiding the player, then following, then backing away
			if (playerBetween.magnitude < innerDist)
			{
				physics.Acceleration = new Vector3 (playerBetween.x, playerBetween.y).normalized * -1f;
				return;
			}
			if (chatPartner != null)
			{
				Vector3 chatterBetween = chatPartner.position - self.position;
				if (chatterBetween.magnitude < innerDist)
				{
					physics.Acceleration = new Vector3 (chatterBetween.x, chatterBetween.y).normalized * -1f;
					return;
				}
			}
			if (target != null)
			{
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
			face.MakeSad ();
		}
	}
}
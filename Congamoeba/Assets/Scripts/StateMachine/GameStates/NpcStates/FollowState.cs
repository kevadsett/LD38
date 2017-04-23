using UnityEngine;
using System.Collections;
using Congamoeba.GameStateMachine;
using Congamoeba.Player;

namespace Congamoeba.NPC {
	public class FollowState : IGameState {
		public Camera StateCamera { get { return null; } }

		Transform self;
		Transform target;
		PlayerPhysics physics;

		const float outerDist = 2f;
		const float innerDist = 1.5f;

		public FollowState (GameObject npc) {
			self = npc.transform;
			target = Congamoeba.Player.PlayerMovementController.PlayerTransform;
			physics = npc.GetComponent<PlayerPhysics> ();
		}

		public void OnEnter() {
			
		}

		public void Update () {
			Vector3 targetPos = target.position;
			Vector3 between = targetPos - self.position;

			if (between.magnitude > outerDist)
			{
				physics.Acceleration = new Vector2 (between.x, between.y).normalized;
			}
			else if (between.magnitude < innerDist)
			{
				//physics.Acceleration = new Vector2 (between.x, between.y).normalized;
			}
		}

		public void OnExit () {
		}
	}
}
using UnityEngine;
using Congamoeba.GameStateMachine;
using Congamoeba.NPC;

namespace Congamoeba.Player
{
	public class CollisionStateChanger : MonoBehaviour {
		public Transform ConversationTarget;

		void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.layer != LayerMask.NameToLayer("NPC"))
			{
				return;
			}

			NpcStateMachine npcStateMachine = collision.gameObject.GetComponent<NpcStateMachine> ();
			if (npcStateMachine.CurrentStateType == eNpcState.Following)
			{
				return;
			}

			npcStateMachine.ChangeState (eNpcState.Conversation);

			ConversationMover conversationMover = collision.gameObject.GetComponent<ConversationMover> ();
			ConversationState.ConversationPartner = conversationMover;

			GameStateMachine.GameStateMachine.ChangeState (eGameState.Conversation);
		}
	}
}

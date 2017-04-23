using System;
using Congamoeba.Conversations;

namespace Congamoeba.GameStateMachine
{
	public class LoadState : IGameState
	{
		public UnityEngine.Camera StateCamera { get { return null; } }

		public void OnEnter ()
		{
			ConversationService.Initialise ();
			GameStateMachine.ChangeState (eGameState.FreeMove);
		}

		public void Update ()
		{
		}

		public void OnExit ()
		{
		}
	}
}

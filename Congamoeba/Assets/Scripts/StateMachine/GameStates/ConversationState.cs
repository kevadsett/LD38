using System;
using UnityEngine;

namespace Congamoeba.GameStateMachine
{
	public class ConversationState : IGameState
	{
		public Camera StateCamera { get; private set; }

		public ConversationState(Camera stateCamera)
		{
			StateCamera = stateCamera;
		}

		public void OnEnter()
		{
		}

		public void Update()
		{
		}

		public void OnExit()
		{
		}
	}
}


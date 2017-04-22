using System;
using Congamoeba.GameStateMachine;
using UnityEngine;

namespace Congamoeba.NPC
{
	public class IdlingState : IGameState
	{
		public Camera StateCamera { get { return null; } }

		private GameObject _npc;

		public IdlingState (GameObject npc)
		{
			_npc = npc;
		}

		public void OnEnter()
		{
		}

		public void Update ()
		{
		}

		public void OnExit ()
		{
		}
	}
}


using System;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.NPC
{
	public enum eNpcState
	{
		Idling
	};

	public class NpcStateMachine : MonoBehaviour
	{
		public Dictionary<eNpcState, IGameState> _states;

		void Awake()
		{
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) }
			};
		}
	}
}


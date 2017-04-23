using System;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.NPC
{
	public enum eNpcState
	{
		Idling,
		Talking,
		Listening,
		Reacting,
		Following,
		Merging
	};

	public class NpcStateMachine : MonoBehaviour
	{
		public Dictionary<eNpcState, IGameState> _states;

		private IGameState _currentState;

		void Awake()
		{
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) }
			};
			ChangeState (eNpcState.Idling);
		}

		void ChangeState(eNpcState newState)
		{
			if (_currentState != null)
			{
				_currentState.OnExit ();
			}
			_currentState = _states [newState];
			_currentState.OnEnter ();
		}

		void Update()
		{
			_currentState.Update ();
		}
	}
}


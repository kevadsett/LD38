using System;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;
using Congamoeba.Player;

namespace Congamoeba.NPC
{
	public enum eNpcState
	{
		Idling,
		Conversation,
		Following,
		Merging
	};

	public class NpcStateMachine : MonoBehaviour
	{
		public Dictionary<eNpcState, IGameState> _states;

		public List<AudioClip> YaySounds;
		public List<AudioClip> NaySounds;

		private IGameState _currentState;

		public int Difficulty;

		void Awake()
		{
			PlayerSounds playerSounds = GameObject.Find ("Player").GetComponent<PlayerSounds> ();
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) },
				{ eNpcState.Conversation, new NpcConversationState (this, YaySounds, NaySounds, playerSounds) },
//				{ eNpcState.Following, new FollowingState() }
			};
			ChangeState (eNpcState.Idling);
		}

		public void ChangeState(eNpcState newState)
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


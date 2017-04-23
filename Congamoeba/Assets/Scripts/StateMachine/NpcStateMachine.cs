﻿using System;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;

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
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) },
				{ eNpcState.Conversation, new NpcConversationState (this, YaySounds, NaySounds) },
				{ eNpcState.Following, new FollowState (gameObject) },
			};
			ChangeState (eNpcState.Following);
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


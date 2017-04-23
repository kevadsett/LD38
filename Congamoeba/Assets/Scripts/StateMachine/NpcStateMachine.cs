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

	public class VoiceData
	{
		public int Id;
		public float Pitch;
	}

	public class NpcStateMachine : MonoBehaviour
	{
		public Dictionary<eNpcState, IGameState> _states;

		public List<AudioClip> YaySounds;
		public List<AudioClip> NaySounds;

		public eNpcState CurrentStateType;

		public int NpcVoiceCount;

		public int Difficulty;

		public VoiceData VoiceSettings;

		private IGameState _currentState;

		void Awake()
		{
			PlayerSounds playerSounds = GameObject.Find ("Player").GetComponent<PlayerSounds> ();
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) },
				{ eNpcState.Conversation, new NpcConversationState (this, YaySounds, NaySounds, playerSounds) },
				{ eNpcState.Following, new FollowState (gameObject) }
			};
			VoiceSettings = new VoiceData
			{
				Id = Random.Range (0, NpcVoiceCount - 1),
				Pitch = 1 + (Random.value * 0.2f - 0.1f)
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

			CurrentStateType = newState;
			_currentState.OnEnter ();
		}

		void Update()
		{
			_currentState.Update ();
		}
	}
}


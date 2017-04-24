using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;
using Congamoeba.Player;
using Congamoeba.Conversations;

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

		public SyllableData YaySound;
		public SyllableData NaySound;

		public eNpcState CurrentStateType;

		public int NpcVoiceCount;

		public Transform PlayerTransform;

		public VoiceData VoiceSettings;

		public float KillDistance;

		private IGameState _currentState;

		void Awake()
		{
			PlayerSounds playerSounds = GameObject.Find ("Player").GetComponent<PlayerSounds> ();
			_states = new Dictionary<eNpcState, IGameState> {
				{ eNpcState.Idling, new IdlingState (gameObject) },
				{ eNpcState.Conversation, new NpcConversationState (this, YaySound, NaySound, playerSounds) },
				{ eNpcState.Following, new FollowState (gameObject) }
			};
			VoiceSettings = new VoiceData
			{
				Id = Random.Range (0, NpcVoiceCount),
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
			Vector3 distanceToPlayer = transform.position - PlayerTransform.position;
			if (Mathf.Abs (distanceToPlayer.magnitude) > KillDistance)
			{
				NpcGeneration.Kill (this);
				NpcGeneration.GenerateNewNpc ();
			}
		}
	}
}


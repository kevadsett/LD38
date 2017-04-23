using Congamoeba.Conversations;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;
using Congamoeba.Player;

namespace Congamoeba.NPC
{
	public class NpcConversationState : IGameState
	{
		public Camera StateCamera { get { return null; } }

		private NpcStateMachine _stateMachine;

		private ConversationData _conversation;

		private enum eConversationState
		{
			talking,
			listening,
			reacting
		}

		private enum eReactionType
		{
			moreInfoNeeded,
			yay,
			nay
		}

		private eConversationState _currentState;

		private eReactionType _reaction;

		private AudioSource _audioSource;

		private int _stringPosition;


		private int _sentenceIndex;

		private int _syllableIndex;

		private List<AudioClip> _yaySounds;
		private List<AudioClip> _naySounds;

		private PlayerSounds _playerSounds;

		private float _timeReactingStarted;

		public NpcConversationState (
			NpcStateMachine npcStateMachine,
			List<AudioClip> yaySounds,
			List<AudioClip> naySounds,
			PlayerSounds playerSounds
		) {
			_conversation = ConversationService.GetConversation (npcStateMachine.Difficulty);
			_currentState = eConversationState.talking;
			_audioSource = npcStateMachine.gameObject.GetComponent<AudioSource> ();
			_stateMachine = npcStateMachine;
			_yaySounds = yaySounds;
			_naySounds = naySounds;
			_playerSounds = playerSounds;
		}

		public void OnEnter()
		{
			_stringPosition = 0;
			_reaction = eReactionType.moreInfoNeeded;
			_currentState = eConversationState.talking;
			_sentenceIndex = 0;
			_syllableIndex = 0;
		}

		public void Update()
		{
			switch (_currentState)
			{
			case eConversationState.talking:
				_playerSounds.Disable ();
				Speak ();
				break;
			case eConversationState.listening:
				_playerSounds.Enable ();
				JudgeKeys ();
				if (_reaction != eReactionType.moreInfoNeeded)
				{
					_timeReactingStarted = Time.time;
					_currentState = eConversationState.reacting;
				}
				break;
			case eConversationState.reacting:
				_playerSounds.Disable ();
				if (Time.time - _timeReactingStarted < ConversationService.REACTION_SPEED)
				{
					return;
				}
				switch (_reaction)
				{
				case eReactionType.yay:
					_audioSource.clip = _yaySounds[Random.Range(0, _yaySounds.Count - 1)];
					Debug.Log ("Yay");
//					_stateMachine.ChangeState (eNpcState.Following);
					_stateMachine.ChangeState (eNpcState.Idling);
					break;
				case eReactionType.nay:
					_audioSource.clip = _naySounds[Random.Range(0, _naySounds.Count - 1)];
					Debug.Log ("Nay");
					_stateMachine.ChangeState (eNpcState.Idling);
					break;
				}
				_audioSource.Play ();
				GameStateMachine.GameStateMachine.ChangeState (eGameState.FreeMove);
				break;
			}
		}

		public void OnExit()
		{
			_playerSounds.Enable ();
		}

		private void Speak ()
		{
			if (_audioSource.isPlaying)
			{
				return;
			}

			SentenceData sentence = _conversation.Sentences [_sentenceIndex];
			SyllableData syllable = sentence.Syllables [_syllableIndex];

			_audioSource.clip = syllable.NpcAudioClip;
			_audioSource.Play ();

			if (_syllableIndex < sentence.Syllables.Count - 1)
			{
				_syllableIndex++;
			}
			else
			{
				_currentState = eConversationState.listening;
			}
		}

		private void JudgeKeys()
		{
			if (Input.anyKeyDown == false)
			{
				return;
			}

			SentenceData sentence = _conversation.Sentences [_sentenceIndex];

			if (Input.GetButtonDown (sentence.Syllables [_stringPosition].Input))
			{
				_stringPosition++;
				if (_stringPosition == sentence.Syllables.Count)
				{
					_reaction = eReactionType.yay;
				}
			}
			else
			{
				_reaction = eReactionType.nay;
			}
		}
	}
}

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

		private SyllableData _yaySound;
		private SyllableData _naySound;

		private PlayerSounds _playerSounds;

		private float _timeReactingStarted;

		private CuteWeeFace _face;

		public NpcConversationState (
			NpcStateMachine npcStateMachine,
			SyllableData yaySound,
			SyllableData naySound,
			PlayerSounds playerSounds
		) {
			_currentState = eConversationState.talking;
			_audioSource = npcStateMachine.gameObject.GetComponent<AudioSource> ();
			_stateMachine = npcStateMachine;
			_yaySound = yaySound;
			_naySound = naySound;
			_playerSounds = playerSounds;

			_face = npcStateMachine.GetComponentInChildren<CuteWeeFace> ();
		}

		public void OnEnter()
		{
			_conversation = ConversationService.GetConversation ();
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
				if (Time.time - _timeReactingStarted < ConversationService.REACTION_SPEED)
				{
					return;
				}
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
					_audioSource.clip = ConversationService.GetNpcClip (_yaySound.name, _stateMachine.VoiceSettings);
					_stateMachine.ChangeState (eNpcState.Following);
					_playerSounds.PlaySuccess ();
					break;
				case eReactionType.nay:
					_audioSource.clip = ConversationService.GetNpcClip(_naySound.name, _stateMachine.VoiceSettings);
					_stateMachine.ChangeState (eNpcState.Idling);
					break;
				}
				_audioSource.Play ();
				_face.SayWord ();

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

			SentenceData sentence = _conversation.NpcSentences [_sentenceIndex];
			SyllableData syllable = sentence.Syllables [_syllableIndex];

			_audioSource.clip = ConversationService.GetNpcClip (syllable.name, _stateMachine.VoiceSettings);
			_audioSource.Play ();

			_face.SayWord ();

			if (_syllableIndex < sentence.Syllables.Count - 1)
			{
				_syllableIndex++;
			}
			else
			{
				_currentState = eConversationState.listening;
			}
		}

		private bool AnyKeyDown()
		{
			return Input.GetButtonDown ("Sfx0") || Input.GetButtonDown ("Sfx1")
				|| Input.GetButtonDown ("Sfx2") || Input.GetButtonDown ("Sfx3")
				|| Input.GetButtonDown ("Sfx4");
		}

		private void JudgeKeys()
		{
			if (AnyKeyDown() == false)
			{
				return;
			}

			SentenceData sentence = _conversation.PlayerSentences [_sentenceIndex];

			if (Input.GetButtonDown (sentence.Syllables [_stringPosition].Input))
			{
				_stringPosition++;
				if (_stringPosition == sentence.Syllables.Count)
				{
					_sentenceIndex++;
					if (_sentenceIndex >= _conversation.NpcSentences.Count)
					{
						_reaction = eReactionType.yay;
					}
					else
					{
						_currentState = eConversationState.talking;
						_timeReactingStarted = Time.time;
						_stringPosition = 0;
						_syllableIndex = 0;
					}
					ConversationService.IncreaseDifficulty ();
				}
			}
			else
			{
				_reaction = eReactionType.nay;
			}
		}
	}
}

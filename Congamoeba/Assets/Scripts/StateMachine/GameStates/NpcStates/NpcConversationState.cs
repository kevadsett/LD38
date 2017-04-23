using Congamoeba.Conversations;
using Congamoeba.GameStateMachine;
using UnityEngine;
using System.Collections.Generic;

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

		private bool _startedSpeaking;

		private int _sentenceIndex;

		private int _syllableIndex;

		private List<AudioClip> _yaySounds;
		private List<AudioClip> _naySounds;

		public NpcConversationState (NpcStateMachine npcStateMachine, List<AudioClip> yaySounds, List<AudioClip> naySounds)
		{
			_conversation = ConversationService.GetConversation (npcStateMachine.Difficulty);
			_currentState = eConversationState.talking;
			_audioSource = npcStateMachine.gameObject.GetComponent<AudioSource> ();
			_stateMachine = npcStateMachine;
			_yaySounds = yaySounds;
			_naySounds = naySounds;
		}

		public void OnEnter()
		{
			_stringPosition = 0;
			_reaction = eReactionType.moreInfoNeeded;
			_currentState = eConversationState.talking;
			_startedSpeaking = false;
			_sentenceIndex = 0;
			_syllableIndex = 0;
		}

		public void Update()
		{
			switch (_currentState)
			{
			case eConversationState.talking:
				Speak ();
				break;
			case eConversationState.listening:
				JudgeKeys ();
				if (_reaction != eReactionType.moreInfoNeeded)
				{
					_currentState = eConversationState.reacting;
				}
				break;
			case eConversationState.reacting:
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
		}

		private void Speak ()
		{
			_startedSpeaking = true;
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
			if (Input.anyKeyDown)
			{
				if (Input.inputString.Length == 0)
				{
					return;
				}

				char inputChar = Input.inputString.ToLower ().ToCharArray()[0];

				if (((inputChar >= 'a' && inputChar <= 'z') || (inputChar >= '0' && inputChar <= '9')) == false)
				{
					return;
				}

				string sentenceString = ConversationService.GetSentenceString (_conversation.Sentences [_sentenceIndex]);

				Debug.Log (string.Format("Got {0}. Expecting {1}", Input.inputString, sentenceString[_stringPosition]));
				if (inputChar == sentenceString[_stringPosition])
				{
					_stringPosition++;
					if (_stringPosition == sentenceString.Length)
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
}

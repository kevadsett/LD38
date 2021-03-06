﻿using Congamoeba.Conversations;
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
			if (_stateMachine.Conversation == null)
			{
				_stateMachine.Conversation = ConversationService.GetConversation ();
			}
			_conversation = _stateMachine.Conversation;
			_stringPosition = 0;
			_reaction = eReactionType.moreInfoNeeded;
			_currentState = eConversationState.talking;
			_sentenceIndex = 0;
			_syllableIndex = 0;

			AudioPlayer.PlaySound ("bassnote0");
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
					NpcGeneration.Ignore (_stateMachine);
//					Vector3 scale = _playerSounds.gameObject.transform.localScale;
//					_playerSounds.gameObject.transform.localScale = new Vector3 (scale.x * 1.5f, scale.y * 1.5f, scale.z);
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

			ButtonUnlocker.UnlockButton (syllable.Input);
			SyllableIcon.RevealIcon (_syllableIndex, sentence.Syllables.Count, syllable.Sprite);

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
			return ButtonUnlocker.GetButtonDown ("Sfx0") || ButtonUnlocker.GetButtonDown ("Sfx1")
				|| ButtonUnlocker.GetButtonDown ("Sfx2") || ButtonUnlocker.GetButtonDown ("Sfx3")
				|| ButtonUnlocker.GetButtonDown ("Sfx4");
		}

		private void JudgeKeys()
		{
			if (AnyKeyDown() == false)
			{
				return;
			}

			SentenceData sentence = _conversation.PlayerSentences [_sentenceIndex];
			SyllableData syllable = sentence.Syllables [_stringPosition];

			if (Input.GetButtonDown (syllable.Input))
			{
				SyllableIcon.RevealIcon (_stringPosition, sentence.Syllables.Count, syllable.Sprite);

				_stringPosition++;
				if (_stringPosition == sentence.Syllables.Count)
				{
					_sentenceIndex++;
					if (_sentenceIndex >= _conversation.NpcSentences.Count)
					{
						_reaction = eReactionType.yay;

						AudioPlayer.PlaySound ("treblenote0");
						ConversationService.IncreaseDifficulty ();

						SyllableIcon.ResetIcons ();
					}
					else
					{
						_currentState = eConversationState.talking;
						_timeReactingStarted = Time.time;
						_stringPosition = 0;
						_syllableIndex = 0;

						AudioPlayer.PlaySound ("bassnote" + _sentenceIndex);
					}
				}
			}
			else
			{
				_reaction = eReactionType.nay;

				SyllableIcon.ResetIcons ();
			}
		}
	}
}

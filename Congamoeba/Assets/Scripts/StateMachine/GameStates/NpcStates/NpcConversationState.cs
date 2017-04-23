using System;
using Congamoeba.Conversations;
using Congamoeba.GameStateMachine;
using UnityEngine;

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

		public NpcConversationState (NpcStateMachine npcStateMachine)
		{
			_conversation = ConversationService.GetConversation ("Hi");
			_currentState = eConversationState.talking;
			_audioSource = npcStateMachine.gameObject.GetComponent<AudioSource> ();
			_stateMachine = npcStateMachine;
		}

		public void OnEnter()
		{
			_stringPosition = 0;
			_reaction = eReactionType.moreInfoNeeded;
			_currentState = eConversationState.talking;
		}

		public void Update()
		{
			switch (_currentState)
			{
			case eConversationState.talking:
				Speak ();
				Debug.Log (_conversation.name);
				if (_audioSource.isPlaying == false)
				{
					_currentState = eConversationState.listening;
				}
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
					Debug.Log ("Yay");
//					_stateMachine.ChangeState (eNpcState.Following);
					_stateMachine.ChangeState (eNpcState.Idling);
					break;
				case eReactionType.nay:
					Debug.Log ("Nay");
					_stateMachine.ChangeState (eNpcState.Idling);
					break;
				}
				GameStateMachine.GameStateMachine.ChangeState (eGameState.FreeMove);
				break;
			}
		}

		public void OnExit()
		{
		}

		private void Speak ()
		{
			if (_conversation.AudioClip != null)
			{
				_audioSource.clip = _conversation.AudioClip;
				_audioSource.Play ();
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

				Debug.Log (string.Format("Got {0}. Expecting {1}", Input.inputString, _conversation.String[_stringPosition]));
				if (Input.inputString.ToLower().ToCharArray()[0] == _conversation.String.ToLower()[_stringPosition])
				{
					_stringPosition++;
					if (_stringPosition == _conversation.String.Length)
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

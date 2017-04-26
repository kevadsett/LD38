using System;
using UnityEngine;
using Congamoeba.NPC;

namespace Congamoeba.GameStateMachine
{
	public class ConversationState : IGameState
	{
		private static ConversationMover _conversationPartner;
		public static ConversationMover ConversationPartner
		{
			get
			{
				return _conversationPartner;
			}
			set
			{
				_conversationPartner = value;
				if (value != null)
				{
					ChatPartnerTransform = _conversationPartner.transform;
				}
			}
		}
		public static Transform ChatPartnerTransform;

		public Camera StateCamera { get; private set; }

		private GameObject _player;
		private ConversationMover _playerConversationMover;

		public ConversationState(Camera stateCamera, GameObject player)
		{
			StateCamera = stateCamera;
			_player = player;
		}

		public void OnEnter()
		{
			ConversationPartner.Enable ();
			ConversationPartner.MoveIntoConversation (_player.transform);
			if (_playerConversationMover == null)
			{
				_playerConversationMover = _player.GetComponent<ConversationMover> ();
			}
			_playerConversationMover.Enable ();
			_playerConversationMover.MoveIntoConversation(_player.transform);
		}

		public void Update()
		{
		}

		public void OnExit()
		{
			ConversationPartner.Disable ();

			if (_player != null &&_playerConversationMover == null)
			{
				_playerConversationMover = _player.GetComponent<ConversationMover> ();
			}

			if (_playerConversationMover != null)
			{
				_playerConversationMover.Disable ();
			}
			ConversationPartner = null;
			ChatPartnerTransform = null;
		}
	}
}


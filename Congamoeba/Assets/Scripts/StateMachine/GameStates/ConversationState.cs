using System;
using UnityEngine;
using Congamoeba.NPC;

namespace Congamoeba.GameStateMachine
{
	public class ConversationState : IGameState
	{
		public static ConversationMover ConversationPartner;

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
			ConversationPartner.MoveIntoConversation (_player.transform.position);
			if (_playerConversationMover == null)
			{
				_playerConversationMover = _player.GetComponent<ConversationMover> ();
			}
			_playerConversationMover.Enable ();
			_playerConversationMover.MoveIntoConversation(_player.transform.position);
		}

		public void Update()
		{
		}

		public void OnExit()
		{
			ConversationPartner.MoveOutOfConversation ();
			ConversationPartner.Disable ();

			if (_playerConversationMover == null)
			{
				_playerConversationMover = _player.GetComponent<ConversationMover> ();
			}
			_playerConversationMover.Disable ();
		}
	}
}


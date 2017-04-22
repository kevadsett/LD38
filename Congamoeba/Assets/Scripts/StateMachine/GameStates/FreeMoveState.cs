using UnityEngine;
using Congamoeba.Player;

namespace Congamoeba.GameStateMachine
{
	public class FreeMoveState : IGameState
	{
		private PlayerMovementController _playerMovementController;

		public Camera StateCamera { get; private set; }

		private GameObject _player;

		public FreeMoveState(Camera stateCamera, GameObject player)
		{
			StateCamera = stateCamera;
			_player = player;
		}

		public void OnEnter()
		{
			if (_playerMovementController == null)
			{
				_playerMovementController = _player.GetComponent<PlayerMovementController> ();
			}

			_playerMovementController.Start ();
		}

		public void Update()
		{
			
		}

		public void OnExit()
		{
			_playerMovementController.Stop();
		}
	}
}


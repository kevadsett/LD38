using UnityEngine;
using Congamoeba.Player;

namespace Congamoeba.GameStateMachine
{
	public class FreeMoveState : IGameState
	{
		private PlayerMovementController _playerMovementController;

		public Camera StateCamera { get; private set; }

		private GameObject _player;

		private PlayerSounds _playerSounds;

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

			if (_playerSounds == null)
			{
				_playerSounds = _player.GetComponent<PlayerSounds> ();
			}

			_playerSounds.Enable ();

			_player.GetComponent<CollisionStateChanger> ().enabled = false;
		}

		public void Update()
		{
			
		}

		public void OnExit()
		{
			if (_playerMovementController != null)
			{
				_playerMovementController.Stop ();
			}

			if (_player != null)
			{
				_player.GetComponent<CollisionStateChanger> ().enabled = false;
			}
		}
	}
}


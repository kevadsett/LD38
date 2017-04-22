using UnityEngine;
using Congamoeba.Player;

namespace Congamoeba.GameStateMachine
{
	public class FreeMoveState : IGameState
	{
		private PlayerMovementController _playerMovementController;

		public Camera StateCamera { get; private set; }

		public FreeMoveState(Camera stateCamera)
		{
			StateCamera = stateCamera;
		}

		public void OnEnter()
		{
			if (_playerMovementController == null)
			{
				GameObject player = GameObject.Find ("Player");
				_playerMovementController = player.GetComponent<PlayerMovementController> ();
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


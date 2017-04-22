using UnityEngine;
using Congamoeba.Player;

namespace Congamoeba.GameStateMachine
{
	public class FreeMoveState : IGameState
	{
		private PlayerMovementController _playerMovementController;

		public void OnEnter()
		{
			if (_playerMovementController == null)
			{
				GameObject player = GameObject.Find ("Player");
				_playerMovementController = player.GetComponent<PlayerMovementController> ();
			}

			_playerMovementController.EnableInput ();
		}

		public void Update()
		{
			
		}

		public void OnExit()
		{
			_playerMovementController.EnableInput ();
		}
	}
}


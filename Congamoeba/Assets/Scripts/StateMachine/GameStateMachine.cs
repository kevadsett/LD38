using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.GameStateMachine
{
	public enum eGameState
	{
		Load,
		FreeMove,
		Conversation
	};

	public class GameStateMachine : MonoBehaviour {
		public MixableCamera MixableCamera;

		public Camera FreeMoveCamera;
		public Camera ConversationCamera;

		public GameObject Player;

		public IGameState CurrentState;

		private eGameState _currentStateType;

		private static GameStateMachine _instance;

		private Dictionary<eGameState, IGameState> _states;

		public static void ChangeState(eGameState newState)
		{
			if (_instance != null)
			{
				_instance.ChangeGameState (newState);
			}
		}

		private void ChangeGameState(eGameState newState)
		{
			Debug.Log(string.Format("Changing game state{0} to {1}", (CurrentState != null ? " from " + _currentStateType : ""), newState));
			if (CurrentState != null)
			{
				CurrentState.OnExit ();
				MixableCamera.FromCamera = CurrentState.StateCamera;
			}
			CurrentState = _states [newState];
			_currentStateType = newState;
			CurrentState.OnEnter ();

			if (CurrentState.StateCamera != null)
			{
				MixableCamera.ToCamera = CurrentState.StateCamera;

				MixableCamera.Reset ();
			}
		}

		void OnEnable()
		{
			_instance = this;
			_states = new Dictionary<eGameState, IGameState>
			{
				{ eGameState.Load, new LoadState() },
				{ eGameState.FreeMove, new FreeMoveState(FreeMoveCamera, Player) },
				{ eGameState.Conversation, new ConversationState(ConversationCamera, Player) }
			};
			ChangeGameState (eGameState.Load);
		}

		void Update()
		{
			CurrentState.Update ();
		}

		void OnDisable()
		{
			CurrentState.OnExit ();
			CurrentState = null;
			_instance = null;
		}
	}
}

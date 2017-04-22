using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.GameStateMachine
{
	public enum eGameState
	{
		FreeMove,
		Conversation
	};


	public class GameStateMachine : MonoBehaviour {
		public IGameState CurrentState;
		private eGameState _currentStateType;

		private static GameStateMachine _instance;

		private Dictionary<eGameState, IGameState> _states = new Dictionary<eGameState, IGameState>
		{
			{ eGameState.FreeMove, new FreeMoveState() },
			{ eGameState.Conversation, new ConversationState() }
		};

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
			}
			CurrentState = _states [newState];
			_currentStateType = newState;
			CurrentState.OnEnter ();
		}

		void OnEnable()
		{
			_instance = this;
			ChangeGameState (eGameState.FreeMove);
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

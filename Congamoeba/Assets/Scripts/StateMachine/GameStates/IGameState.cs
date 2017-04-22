using System;

namespace Congamoeba.GameStateMachine
{
	public interface IGameState
	{
		void OnEnter();
		void Update();
		void OnExit();
	}
}


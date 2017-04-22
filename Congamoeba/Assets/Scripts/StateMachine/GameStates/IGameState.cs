using System;
using UnityEngine;

namespace Congamoeba.GameStateMachine
{
	public interface IGameState
	{
		Camera StateCamera { get; }
		void OnEnter();
		void Update();
		void OnExit();
	}
}


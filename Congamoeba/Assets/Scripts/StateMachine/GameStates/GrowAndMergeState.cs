using System;
using UnityEngine;

namespace Congamoeba.GameStateMachine
{
	public class GrowAndMergeState : IGameState
	{
		public void OnEnter ()
		{

		}

		public void Update ()
		{

		}

		public void OnExit ()
		{
		}

		private Camera _camera;
		public Camera StateCamera { get { return _camera; } }

		public GrowAndMergeState (Camera growAndMergeCamera, GameObject Player)
		{
			_camera = growAndMergeCamera;
		}
	}
}

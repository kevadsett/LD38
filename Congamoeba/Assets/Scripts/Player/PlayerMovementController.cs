using UnityEngine;

namespace Congamoeba.Player
{
	[RequireComponent(typeof(PlayerPhysics))]
	public class PlayerMovementController : MonoBehaviour
	{
		public float Speed;

		private bool _isEnabled;
		private PlayerPhysics physics;

		public void EnableInput()
		{
			_isEnabled = true;
		}

		public void DisableInput()
		{
			_isEnabled = false;
		}

		void Awake()
		{
			physics = GetComponent<PlayerPhysics> ();
		}

		void Update ()
		{
			if (_isEnabled == false)
			{
				return;
			}

			float xAcc = 0;
			float yAcc = 0;

			if (Input.GetKey (KeyCode.W))
			{
				yAcc = 1;
			}
			if (Input.GetKey (KeyCode.A))
			{
				xAcc = -1;
			}
			if (Input.GetKey (KeyCode.D))
			{
				xAcc = 1;
			}
			if (Input.GetKey (KeyCode.S))
			{
				yAcc = -1;
			}

			physics.Acceleration = new Vector2(xAcc, yAcc);
		}
	}
}

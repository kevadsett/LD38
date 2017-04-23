using UnityEngine;

namespace Congamoeba.Player
{
	public class PlayerPhysics : MonoBehaviour
	{
		[HideInInspector]
		public Vector2 Acceleration;

		[RangeAttribute(0, 1)]
		public float AccelRate;

		[RangeAttribute(0, 1)]
		public float Damping;

		[RangeAttribute(0, 1)]
		public float MaxVelocity;

		public AmoebaFace AmoebaGraphics;

		private Vector2 _velocity;

		void Update ()
		{
			float dt = Time.deltaTime;

			if (Acceleration.sqrMagnitude > 1f) {
				Acceleration = Acceleration.normalized;
			}

			_velocity += Acceleration * AccelRate * dt;

			_velocity *= Mathf.Pow(Damping, dt);
			
			if (_velocity.magnitude > MaxVelocity) {
				_velocity = _velocity.normalized * MaxVelocity;
			}

			if (AmoebaGraphics != null) {
				AmoebaGraphics.UpdateDirection (_velocity);
			}

			transform.position = new Vector3(transform.position.x + _velocity.x, transform.position.y + _velocity.y);
		}

		public void Stop()
		{
			Acceleration = Vector2.zero;
			_velocity = Vector2.zero;
		}
	}
}

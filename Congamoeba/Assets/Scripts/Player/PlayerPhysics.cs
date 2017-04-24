using UnityEngine;

namespace Congamoeba.Player
{
	public class PlayerPhysics : MonoBehaviour
	{
		[HideInInspector]
		public Vector3 Acceleration;

		[RangeAttribute(0, 100)]
		public float AccelRate;

		[RangeAttribute(0, 1)]
		public float Damping;

		[RangeAttribute(0, 100)]
		public float MaxVelocity;

		public AmoebaFace AmoebaGraphics;

		public Vector3 Velocity;

		void Update ()
		{
			float dt = Time.deltaTime;

			if (Acceleration.sqrMagnitude > 1f) {
				Acceleration = Acceleration.normalized;
			}

			Velocity += Acceleration * AccelRate * dt;

			Velocity *= Mathf.Pow(Damping, dt);
			
			if (Velocity.magnitude > MaxVelocity) {
				Velocity = Velocity.normalized * MaxVelocity;
			}

			if (AmoebaGraphics != null) {
				AmoebaGraphics.UpdateDirection (Velocity);
			}

			transform.position += Velocity * Time.deltaTime * transform.localScale.x;
		}

		public void Stop()
		{
			Acceleration = Vector2.zero;
			Velocity = Vector2.zero;
		}
	}
}

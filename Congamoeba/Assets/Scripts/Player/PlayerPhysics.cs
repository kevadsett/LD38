using UnityEngine;

namespace Congamoeba.Player
{
	public class PlayerPhysics : MonoBehaviour
	{
		public Vector2 Acceleration;

		[RangeAttribute(0, 1)]
		public float Damping;

		[RangeAttribute(0, 1)]
		public float MaxVelocity;

		private Vector2 _velocity;

		void Update ()
		{
			float dt = Time.deltaTime;

			_velocity += Acceleration * dt;

			_velocity *= Mathf.Pow(Damping, dt);
			
			_velocity.x = Mathf.Clamp (_velocity.x, -MaxVelocity, MaxVelocity);
			_velocity.y = Mathf.Clamp (_velocity.y, -MaxVelocity, MaxVelocity);

			transform.position = new Vector3(transform.position.x + _velocity.x, transform.position.y + _velocity.y);
		}
	}
}

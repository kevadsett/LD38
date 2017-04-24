using UnityEngine;

namespace Congamoeba.Player
{
	public class MovingSound : MonoBehaviour
	{
		public PlayerPhysics Physics;

		private AudioSource _audioSource;

		void Start ()
		{
			_audioSource = GetComponent<AudioSource>();
		}
		

		void Update ()
		{
			_audioSource.volume = Physics.Velocity.magnitude / 5f;
			_audioSource.pitch = 1 + Physics.Velocity.magnitude / 100f;
		}
	}
}

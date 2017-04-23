using UnityEngine;
using Congamoeba.Conversations;

namespace Congamoeba.Player
{
	[RequireComponent(typeof(AudioSource))]
	public class PlayerSounds : MonoBehaviour
	{
		private bool _isEnabled;

		private AudioSource _audioSource;
		private CuteWeeFace _face;

		private bool _shouldPlaySuccessNoise;
		private float _successNoiseDelay;
		private float _timeSuccessNoiseQueued;

		void Awake ()
		{
			_audioSource = GetComponent<AudioSource> ();
			_face = GetComponentInChildren<CuteWeeFace> ();
		}

		void Update()
		{
			if (_shouldPlaySuccessNoise)
			{
				if (Time.time - _timeSuccessNoiseQueued >= _successNoiseDelay)
				{
					ActuallyPlaySuccess ();
				}
				return;
			}

			if (Input.GetButtonDown ("Sfx0"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx0");
				Play ();
			}
			if (Input.GetButtonDown ("Sfx1"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx1");
				Play ();
			}
			if (Input.GetButtonDown ("Sfx2"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx2");
				Play ();
			}
			if (Input.GetButtonDown ("Sfx3"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx3");
				Play ();
			}
			if (Input.GetButtonDown ("Sfx4"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx4");
				Play ();
			}
		}

		public void Enable()
		{
			if (enabled == false)
			{
				enabled = true;
			}
		}

		public void Disable()
		{
			if (enabled == true)
			{
				enabled = false;
			}
		}

		public void PlaySuccess()
		{
			_shouldPlaySuccessNoise = true;
			_timeSuccessNoiseQueued = Time.time;
			_successNoiseDelay = Random.value  * 0.2f + 0.1f;
		}

		private void ActuallyPlaySuccess()
		{
			_audioSource.clip = ConversationService.GetPlayerClip ("Success");
			Play ();
			_shouldPlaySuccessNoise = false;
		}

		private void Play()
		{
			_audioSource.pitch = (Random.value * 0.05f) + 1.025f;
			_audioSource.Play ();
			_face.SayWord ();
		}
	}
}


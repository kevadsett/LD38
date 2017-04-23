using UnityEngine;
using Congamoeba.Conversations;

namespace Congamoeba.NPC
{
	[RequireComponent(typeof(AudioSource))]
	public class PlayerSoundMimic : MonoBehaviour
	{
		public float MaxDelay;

		private AudioSource _audioSource;

		private float _playRequestTime;
		private float _delay;
		private bool _shouldPlay;

		void Awake ()
		{
			_audioSource = GetComponent<AudioSource> ();
		}

		void Update()
		{
			if (Input.GetButtonDown ("Sfx0"))
			{
				PlaySound ("Sfx0");
			}
			if (Input.GetButtonDown ("Sfx1"))
			{
				PlaySound ("Sfx1");
			}
			if (Input.GetButtonDown ("Sfx2"))
			{
				PlaySound ("Sfx2");
			}
			if (Input.GetButtonDown ("Sfx3"))
			{
				PlaySound ("Sfx3");
			}
			if (Input.GetButtonDown ("Sfx4"))
			{
				PlaySound ("Sfx4");
			}

			if (_shouldPlay == false)
			{
				return;
			}
			if (Time.time - _playRequestTime >= _delay)
			{
				_shouldPlay = false;
				_audioSource.Play ();
			}
		}

		public void PlaySound(string input)
		{
			_shouldPlay = true;
			_playRequestTime = Time.time;
			_delay = Random.value * MaxDelay;
			_audioSource.clip = ConversationService.GetNpcClip (input);
		}
	}
}


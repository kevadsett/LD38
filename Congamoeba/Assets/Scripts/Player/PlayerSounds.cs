using System;
using UnityEngine;
using Congamoeba.Conversations;

namespace Congamoeba.Player
{
	[RequireComponent(typeof(AudioSource))]
	public class PlayerSounds : MonoBehaviour
	{
		private bool _isEnabled;

		private AudioSource _audioSource;

		void Awake ()
		{
			_audioSource = GetComponent<AudioSource> ();
		}

		void Update()
		{
			if (Input.GetButtonDown ("Sfx0"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx0");
				_audioSource.Play ();
			}
			if (Input.GetButtonDown ("Sfx1"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx1");
				_audioSource.Play ();
			}
			if (Input.GetButtonDown ("Sfx2"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx2");
				_audioSource.Play ();
			}
			if (Input.GetButtonDown ("Sfx3"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx3");
				_audioSource.Play ();
			}
			if (Input.GetButtonDown ("Sfx4"))
			{
				_audioSource.clip = ConversationService.GetPlayerClip ("Sfx4");
				_audioSource.Play ();
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
	}
}


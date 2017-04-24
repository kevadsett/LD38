using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class SyllableData : ScriptableObject
	{
		public AudioClip PlayerAudioClip;
		public List<AudioClip> NpcAudioClips;
		public string Input;
		public Sprite Sprite;
	}
}

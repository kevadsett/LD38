using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class SyllableData : ScriptableObject
	{
		public AudioClip PlayerAudioClip;
		public AudioClip NpcAudioClip;
		public string Input;
	}
}

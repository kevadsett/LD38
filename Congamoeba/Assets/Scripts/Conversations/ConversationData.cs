using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class ConversationData : ScriptableObject
	{
		public List<SentenceData> Sentences;
		public int Difficulty;
	}

	[CreateAssetMenu]
	public class SentenceData : ScriptableObject
	{
		public List<SyllableData> Syllables;
	}

	[CreateAssetMenu]
	public class SyllableData : ScriptableObject
	{
		public AudioClip PlayerAudioClip;
		public AudioClip NpcAudioClip;
		public string Input;
	}
}

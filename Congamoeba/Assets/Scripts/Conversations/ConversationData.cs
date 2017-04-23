using UnityEngine;
using System.Collections;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class ConversationData : ScriptableObject
	{
		public AudioClip AudioClip;
		public string String;
		public int Difficulty;
	}
}

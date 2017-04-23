using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class SentenceData : ScriptableObject
	{
		public List<SyllableData> Syllables;
	}
}

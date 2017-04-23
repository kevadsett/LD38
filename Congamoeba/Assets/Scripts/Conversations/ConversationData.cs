﻿using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	[CreateAssetMenu]
	public class ConversationData : ScriptableObject
	{
		public List<SentenceData> Sentences;
		public int Difficulty;
	}
}

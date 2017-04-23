using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	public static class ConversationService
	{
		private static Dictionary<string, ConversationData> _conversations;

		private static Dictionary<int, List<ConversationData>> _conversationsByDifficulty;

		public static void Initialise()
		{
			_conversations = new Dictionary<string, ConversationData>();
			_conversationsByDifficulty = new Dictionary<int, List<ConversationData>>();

			foreach (ConversationData conversation in Resources.LoadAll<ConversationData> ("Conversations"))
			{
				_conversations.Add(conversation.name, conversation);
				List<ConversationData> conversationList;
				if (_conversationsByDifficulty.TryGetValue (conversation.Difficulty, out conversationList))
				{
					conversationList.Add (conversation);
				}
				else
				{
					_conversationsByDifficulty.Add (
						conversation.Difficulty,
						new List<ConversationData> () { conversation }
					);
				}
			}
		}

		public static ConversationData GetConversation(int difficulty)
		{
			List<ConversationData> conversationList;
			if (_conversationsByDifficulty.TryGetValue (difficulty, out conversationList))
			{
				return conversationList [Random.Range (0, conversationList.Count - 1)];
			}
			else if (difficulty >= 0)
			{
				return GetConversation (difficulty - 1);
			}
			else
			{
				return null;
			}
		}

		public static string GetSentenceString(SentenceData sentence)
		{
			string sentenceString = "";
			foreach (SyllableData syllable in sentence.Syllables)
			{
				sentenceString += syllable.Character;
			}
			return sentenceString.ToLower();
		}
	}
}

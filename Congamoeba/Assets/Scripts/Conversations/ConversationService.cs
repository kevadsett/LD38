using UnityEngine;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	public static class ConversationService
	{
		public const float REACTION_SPEED = 0.5f;
		private static Dictionary<string, ConversationData> _conversations;

		private static Dictionary<int, List<ConversationData>> _conversationsByDifficulty;

		private static Dictionary<string, AudioClip> _playerClips = new Dictionary<string, AudioClip>();
		private static Dictionary<string, AudioClip> _npcClips = new Dictionary<string, AudioClip>();

		private static int _difficulty;

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

				foreach (SentenceData sentence in conversation.PlayerSentences)
				{
					foreach (SyllableData syllable in sentence.Syllables)
					{
						if (_playerClips.ContainsKey (syllable.Input) == false)
						{
							_playerClips.Add (syllable.Input, syllable.PlayerAudioClip);
						}
						if (_npcClips.ContainsKey (syllable.Input) == false)
						{
							_npcClips.Add (syllable.Input, syllable.NpcAudioClip);
						}
					}
				}
				foreach (SentenceData sentence in conversation.NpcSentences)
				{
					foreach (SyllableData syllable in sentence.Syllables)
					{
						if (_playerClips.ContainsKey (syllable.Input) == false)
						{
							_playerClips.Add (syllable.Input, syllable.PlayerAudioClip);
						}
						if (_npcClips.ContainsKey (syllable.Input) == false)
						{
							_npcClips.Add (syllable.Input, syllable.NpcAudioClip);
						}
					}
				}
			}
		}

		public static ConversationData GetConversation()
		{
			List<ConversationData> conversationList;
			if (_conversationsByDifficulty.TryGetValue (_difficulty, out conversationList))
			{
				return conversationList [Random.Range (0, conversationList.Count - 1)];
			}
			else if (_difficulty >= 0)
			{
				_difficulty--;
				return GetConversation ();
			}
			else
			{
				return null;
			}
		}

		public static AudioClip GetPlayerClip(string input)
		{
			return _playerClips [input];
		}

		public static AudioClip GetNpcClip(string input)
		{
			return _npcClips [input];
		}

		public static void IncreaseDifficulty()
		{
			_difficulty++;
		}
//		public static string GetSentenceString(SentenceData sentence)
//		{
//			string sentenceString = "";
//			foreach (SyllableData syllable in sentence.Syllables)
//			{
//				sentenceString += syllable.Character;
//			}
//			return sentenceString.ToLower();
//		}
	}
}

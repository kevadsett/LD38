using UnityEngine;
using System.Collections.Generic;
using Congamoeba.NPC;

namespace Congamoeba.Conversations
{
	public static class ConversationService
	{
		public const float REACTION_SPEED = 0.5f;
		private static Dictionary<string, ConversationData> _conversations;

		private static Dictionary<int, List<ConversationData>> _conversationsByDifficulty;

		private static Dictionary<string, AudioClip> _playerClips = new Dictionary<string, AudioClip>();
		private static Dictionary<string, List<AudioClip>> _npcClips = new Dictionary<string, List<AudioClip>>();

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
			}

			foreach (SyllableData syllable in Resources.LoadAll<SyllableData> ("Syllables"))
			{
				if (_playerClips.ContainsKey (syllable.Input) == false && syllable.Input != "")
				{
					_playerClips.Add (syllable.Input, syllable.PlayerAudioClip);
				}
				if (_npcClips.ContainsKey (syllable.name) == false)
				{
					List<AudioClip> clipList = new List<AudioClip> ();
					foreach (AudioClip clip in syllable.NpcAudioClips)
					{
						clipList.Add (clip);
					}
					_npcClips.Add (syllable.name, clipList);
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
			Debug.Log (input);
			return _playerClips [input];
		}

		public static AudioClip GetNpcClip(string syllableName, VoiceData npcVoiceData)
		{
			return _npcClips [syllableName][npcVoiceData.Id];
		}

		public static void IncreaseDifficulty()
		{
			_difficulty++;
		}
	}
}

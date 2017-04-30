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

		private static List<SyllableData> _playableSyllables = new List<SyllableData>();
		private static Dictionary<string, SyllableData> _syllablesByName = new Dictionary<string, SyllableData> ();

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
					_playableSyllables.Add (syllable);
				}
				if (_npcClips.ContainsKey (syllable.name) == false)
				{
					List<AudioClip> clipList = new List<AudioClip> ();
					foreach (AudioClip clip in syllable.NpcAudioClips)
					{
						clipList.Add (clip);
					}
					_npcClips.Add (syllable.name, clipList);
					_syllablesByName.Add (syllable.name, syllable);
				}
			}
		}

		public static ConversationData GetConversation()
		{
			//Debug.Log ("Get conversation, difficulty: " + _difficulty);
			List<ConversationData> conversationList;
			if (_conversationsByDifficulty.TryGetValue (_difficulty, out conversationList))
			{
				return conversationList [Random.Range (0, conversationList.Count - 1)];
			}
			else if (_difficulty >= 0)
			{
				return GenerateConversation ();
			}
			else
			{
				return null;
			}
		}

		public static AudioClip GetPlayerClip(string input)
		{
			//Debug.Log (input);
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

		private static ConversationData GenerateConversation()
		{
			ConversationData convo = ScriptableObject.CreateInstance<ConversationData>();
			convo.NpcSentences = new List<SentenceData> ();
			convo.PlayerSentences = new List<SentenceData> ();
			int numberOfSentences = Mathf.Clamp(Random.Range(_difficulty - 2, _difficulty + 2), 2, 5);
			for (int i = 0; i < numberOfSentences; i++)
			{
				SentenceData sentence = ScriptableObject.CreateInstance<SentenceData> ();
				sentence.Syllables = new List<SyllableData> ();
				if (i == numberOfSentences - 1)
				{
					sentence.Syllables.Add (_syllablesByName ["eep"]);
					sentence.Syllables.Add (_syllablesByName ["eep"]);
				}
				else
				{
					int numberOfSyllables = Random.Range (1, Mathf.Clamp(_difficulty - 8, 1, 8));
					for (int j = 0; j < numberOfSyllables; j++)
					{
						SyllableData syllable = _playableSyllables [Random.Range (0, _playableSyllables.Count)];
						while (syllable.name == "success")
						{
							syllable = _playableSyllables [Random.Range (0, _playableSyllables.Count)];
						}
						sentence.Syllables.Add (syllable);
					}
				}
				convo.NpcSentences.Add (sentence);
				convo.PlayerSentences.Add (sentence);
			}

			return convo;
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Congamoeba.Conversations
{
	public class ConversationService
	{
		public static ConversationService Instance { get; private set; }
		private static Dictionary<string, ConversationData> _conversations;

		public static void Initialise()
		{
			_conversations = new Dictionary<string, ConversationData>();

			foreach (ConversationData conversation in Resources.LoadAll<ConversationData> ("Conversations"))
			{
				_conversations.Add(conversation.name, conversation);
			}
		}

		public static ConversationData GetConversation(string name)
		{
			return _conversations [name];
		}
	}
}

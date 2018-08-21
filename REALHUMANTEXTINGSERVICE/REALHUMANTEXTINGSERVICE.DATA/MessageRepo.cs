using Newtonsoft.Json;
using REALHUMANTEXTINGSERVICE.MODELS;
using REALHUMANTEXTINGSERVICE.MODELS.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.DATA
{
	public class MessageRepo : IMessageRepo
	{
		const string messagePath = @"C:\Users\Poor Richard\Desktop\Software Guild\WebPortfolioWork\REALHUMANTEXTINGSERVICE\DataFiles\Messages.json";

		public List<RawMessage> GetAllMessages()
		{
			var allMessages = new List<RawMessage>();

			using (StreamReader reader = new StreamReader(messagePath))
			{
				allMessages = JsonConvert.DeserializeObject<List<RawMessage>>(reader.ReadToEnd());
			}

			return allMessages;
		}

		public RawMessage GetMessage(int messageId)
		{
			return GetAllMessages().SingleOrDefault(m => m.id == messageId);
		}

		public void AddMessage(RawMessage toAdd)
		{
			var allMessages = GetAllMessages();
			allMessages.Add(toAdd);

			var allParsed = JsonConvert.SerializeObject(allMessages);

			File.WriteAllText(messagePath, allParsed);
		}
	}
}

using REALHUMANTEXTINGSERVICE.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.Workflows
{
	public class AllMessagesWorkflow
	{
		public void Execute()
		{
			var manager = ManagerFactory.Create();
			var response = manager.GetAllMessages();

			if (response.Success)
			{
				foreach (var message in response.AllMessages)
				{
					Console.WriteLine($"Message Id: {message.id}");
					Console.WriteLine($"Message Name: {message.messageName}");
					Console.WriteLine($"Message Text: {message.text}");
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine(response.Message);
			}

			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
		}
	}
}

using REALHUMANTEXTINGSERVICE.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.Workflows
{
	public class SendMessageWorkflow
	{
		public void Execute()
		{
			var manager = ManagerFactory.Create();

			Console.Clear();
			Console.Write("Enter the company Id: ");
			var enteredCompanyId = Console.ReadLine();

			Console.Write("Enter the guest Id: ");
			var enteredGuestId = Console.ReadLine();

			Console.Write("Enter the message Id: ");
			var enteredMessageId = Console.ReadLine();

			var response = manager.SendMessage(enteredCompanyId, enteredGuestId, enteredMessageId);

			if (response.Success)
			{
				Console.Clear();
				Console.WriteLine(response.FullTextMessage);
			}
			else
			{
				Console.WriteLine(response.Message);
			}

			Console.ReadKey();
		}
	}
}

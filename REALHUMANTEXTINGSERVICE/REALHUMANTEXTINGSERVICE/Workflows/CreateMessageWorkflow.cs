using REALHUMANTEXTINGSERVICE.BLL;
using REALHUMANTEXTINGSERVICE.MODELS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.Workflows
{
	public class CreateMessageWorkflow
	{
		public void Execute()
		{
			var manager = ManagerFactory.Create();
			var newMessage = new RawMessage();

			Console.Clear();
			Console.Write("Name your message: ");
			newMessage.messageName = Console.ReadLine();

			Console.WriteLine("Enter your full text message below. \n" +
				"To enter a variable, refer to the following list");

			Console.WriteLine("GUEST INFO");
			Console.WriteLine("1: Guest Name : {guestName}");
			Console.WriteLine("2: Greeting : {timeGreeting}");
			Console.WriteLine("COMPANY INFO");
			Console.WriteLine("3: Company : {company}");
			Console.WriteLine("4: City : {city}");
			Console.WriteLine("5: TimeZone : {timezone}");
			Console.WriteLine("RESERVATION INFO");
			Console.WriteLine("6: Room Number : {roomNumber}");
			Console.WriteLine("7: Check In Time : {startTimeStamp}");
			Console.WriteLine("8: Check Out Time : {endTimeStamp}");
			Console.WriteLine();

			newMessage.text = Console.ReadLine();
			if (string.IsNullOrEmpty(newMessage.text) || string.IsNullOrEmpty(newMessage.messageName))
			{
				Console.WriteLine("Invalid entry");
				Console.ReadKey();
				return;
			}

			var allMessagesResponse = manager.GetAllMessages();
			int lastId = 0;
			foreach (var message in allMessagesResponse.AllMessages)
			{
				if (message.id > lastId)
				{
					lastId = message.id;
				}
			}
			newMessage.id = lastId + 1;

			var response = manager.AddMessage(newMessage);

			if (response.Success == true)
			{
				Console.WriteLine("Message saved");
			}
			else
			{
				Console.WriteLine(response.Message);
			}

			Console.ReadKey();
		}
	}
}

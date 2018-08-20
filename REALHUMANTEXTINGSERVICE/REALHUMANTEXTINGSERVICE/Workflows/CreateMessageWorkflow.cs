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

			Console.WriteLine("Enter your full text message below. Enter 'PLACEHOLDER' at any location you want to insert a variable:");
			var userInput = Console.ReadLine();

			if (string.IsNullOrEmpty(userInput) || string.IsNullOrEmpty(newMessage.messageName))
			{
				Console.WriteLine("Invalid entry");
				Console.ReadKey();
				return;
			}

			var splitInput = userInput.Split(' ');

			Console.Clear();
			Console.WriteLine("GUEST INFO");
			Console.WriteLine("1: Guest Name");
			Console.WriteLine("2: Greeting");
			Console.WriteLine("COMPANY INFO");
			Console.WriteLine("3: Company");
			Console.WriteLine("4: City");
			Console.WriteLine("5: TimeZone");
			Console.WriteLine("RESERVATION INFO");
			Console.WriteLine("6: Room Number");
			Console.WriteLine("7: Check In Time");
			Console.WriteLine("8: Check Out Time");
			Console.WriteLine("\n" + userInput + "\n");

			var variables = new List<InsertVariable>();
			for(int i = 0; i < splitInput.Count(); i++)
			{
				if(splitInput[i] == "PLACEHOLDER")
				{
					var newVar = new InsertVariable()
					{
						Index = i
					};

					Console.Write($"Which variable type would you like to add to variable {variables.Count + 1}? Enter it's number: ");
					var varChoice = Console.ReadLine();

					switch (varChoice)
					{
						case "1":
							newVar.Text = "{guestName}";
							break;
						case "2":
							newVar.Text = "{timeGreeting}";
							break;
						case "3":
							newVar.Text = "{company}";
							break;
						case "4":
							newVar.Text = "{city}";
							break;
						case "5":
							newVar.Text = "{timeZone}";
							break;
						case "6":
							newVar.Text = "{roomNumber}";
							break;
						case "7":
							newVar.Text = "{startTimeStamp}";
							break;
						case "8":
							newVar.Text = "{endTimeStamp}";
							break;
						default:
							Console.WriteLine("THAT WASN'T AN OPTION, FRIEND");
							Console.ReadKey();
							return;
					}

					variables.Add(newVar);
				}
			}

			foreach(var variable in variables)
			{
				splitInput[variable.Index] = variable.Text;
			}

			StringBuilder builder = new StringBuilder();
			foreach(var word in splitInput)
			{
				builder.Append((word + " "));
			}
			newMessage.text = builder.ToString();

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

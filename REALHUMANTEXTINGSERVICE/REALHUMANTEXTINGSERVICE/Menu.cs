using REALHUMANTEXTINGSERVICE.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE
{
	public static class Menu
	{
		public static void Start()
		{
			bool open = true;

			while (open)
			{
				Console.Clear();
				Console.WriteLine("HELLO FELLOW HUMAN - LETS TEXT SOME FRIENDS!");
				Console.WriteLine("--------------------------------------------");
				Console.WriteLine("WHAT SHOULD WE DO?");
				Console.WriteLine("1: View Guests \n2: View Companies \n3: View messages \n4: Create Message \n5: Send message \n6: Quit");
				string userInput = Console.ReadLine();

				switch (userInput)
				{
					case "1":
						var viewGuestsWorkflow = new AllGuestsWorkflow();
						viewGuestsWorkflow.Execute();
						break;
					case "2":
						var viewCompaniesWorkflow = new AllCompaniesWorkflow();
						viewCompaniesWorkflow.Execute();
						break;
					case "3":
						var viewMessageWorkflow = new AllMessagesWorkflow();
						viewMessageWorkflow.Execute();
						break;
					case "4":
						var createWorkflow = new CreateMessageWorkflow();
						createWorkflow.Execute();
						break;
					case "5":
						var sendWorkflow = new SendMessageWorkflow();
						sendWorkflow.Execute();
						break;
					case "6":
						open = false;
						break;
					default:
						Console.WriteLine("THAT WASN'T ONE OF THE OPTIONS, CHAMP!");
						Console.ReadKey();
						Console.Clear();
						break;
				}
			}
		}
	}
}

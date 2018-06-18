using FloorOrderingSystem.BLL;
using FOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Models.Responses;

namespace FloorOrderingSystem.Workflows
{
	public class LoadAllOrdersWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Load all orders on a given day");
			Console.WriteLine("-------------------------------");

			DateTime inputOrderDate = new DateTime();
			bool dateVerify = false;
			do
			{
				Console.WriteLine("Enter the order date in the format MM/DD/YYYY");
				string stringDate = Console.ReadLine();

				try
				{
					inputOrderDate = DateTime.Parse(stringDate);
					dateVerify = true;
				}
				catch
				{
					Console.WriteLine("Incorrect format");
					Console.ReadKey();
					Console.Clear();
				}
			} while (!dateVerify);

			AllOrdersResponse response = manager.AllOrderLookup(inputOrderDate);

			if (response.Success)
			{
				foreach (Order order in response.ListOfOrders)
				{
					ConsoleIO.DisplayOrderDetails(order);
					Console.WriteLine("Press any key to continue to next order...");
					Console.ReadKey();
				}
			}
			else
			{
				Console.WriteLine("An error occurred");
				Console.WriteLine(response.Message);
			}

			Console.WriteLine("\nPress any key to continue");
			Console.ReadKey();
		}
	}
}

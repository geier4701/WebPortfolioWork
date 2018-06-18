using FloorOrderingSystem.BLL;
using FOS.Models;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem.Workflows
{
	public class OrderLookupWorkflow
	{
		public Order Execute(bool toEditOrDelete)
		{
			OrderManager manager = OrderManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Load an order");
			Console.WriteLine("------------------");

			int inputOrderNumber = 0;
			bool intVerify = false;
			do
			{
				Console.WriteLine("Enter an order number");

				try
				{
					inputOrderNumber = int.Parse(Console.ReadLine());
					intVerify = true;
				}
				catch
				{
					Console.WriteLine("You must enter a number");
				}

			} while (!intVerify);

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

			OrderLookupResponse response = manager.OrderLookup(inputOrderNumber, inputOrderDate);

			if (response.Success)
			{
				ConsoleIO.DisplayOrderDetails(response.Order);

				if (toEditOrDelete)
				{
					return response.Order;
				}
			}
			else
			{
				Console.WriteLine("An error occurred");
				Console.WriteLine(response.Message);
			}

			Console.WriteLine("\nPress any key to continue");
			Console.ReadKey();
			return null;
		}
	}
}

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
	public class DeleteExistingOrderWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();
			OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
			Order orderToDelete = lookupWorkflow.Execute(true);

			if(orderToDelete != null)
			{
				string deleteResponse;
				bool validInput = false;
				do
				{
					Console.WriteLine("Is this the order you wish to permenantly delete?\nEnter Y/N");
					deleteResponse = Console.ReadLine().ToLower();

					if (deleteResponse == "y" || deleteResponse == "n")
					{
						validInput = true;
					}
				} while (!validInput);

				Console.Clear();

				DeleteOrderResponse response = null;
				if (deleteResponse == "y")
				{
					response = manager.DeleteOrder(orderToDelete);

					if (response.Success)
					{
						Console.WriteLine("Your order was successfully deleted\nPress any key to continue...");
						Console.ReadKey();
					}
					else if (!response.Success)
					{
						Console.WriteLine("An error occurred");
						Console.WriteLine(response.Message);
					}
				}
				else
				{
					Console.WriteLine("\nDelete Order Request Cancelled\nPress any key to continue");
					Console.ReadKey();
				}
			}
		}
	}
}

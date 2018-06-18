using FloorOrderingSystem.Workflows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem
{
	public static class Menu
	{
		public static void Start()
		{
			bool open = true;

			while (open)
			{
				Console.Clear();
				Console.WriteLine("SWC Corp Floor Ordering System");
				Console.WriteLine("---------------------------------");
				Console.WriteLine("Pick from the following options");
				Console.WriteLine("1. Display Order");
				Console.WriteLine("2. Place New Order");
				Console.WriteLine("3. Edit Existing Order");
				Console.WriteLine("4. Delete Existing Order");
				Console.WriteLine("5. Load All Orders On Given Date");
				Console.WriteLine("6. Quit");
				string userInput = Console.ReadLine();

				switch (userInput)
				{
					case "1":
						OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
						lookupWorkflow.Execute(false);
						break;
					case "2":
						PlaceNewOrderWorkflow newOrderWorkflow = new PlaceNewOrderWorkflow();
						newOrderWorkflow.Execute();
						break;
					case "3":
						EditExistingOrderWorkflow editOrderWorkflow = new EditExistingOrderWorkflow();
						editOrderWorkflow.Execute();
						break;
					case "4":
						DeleteExistingOrderWorkflow deleteOrderWorkflow = new DeleteExistingOrderWorkflow();
						deleteOrderWorkflow.Execute();
						break;
					case "5":
						LoadAllOrdersWorkflow allOrdersWorkflow = new LoadAllOrdersWorkflow();
						allOrdersWorkflow.Execute();
						break;
					case "6":
						open = false;
						break;
					default:
						Console.Clear();
						Console.WriteLine("Please enter your choice as a number 1-6\nPress any key to continue...");
						Console.ReadKey();
						Console.Clear();
						break;
				}
			}
		}
	}
}

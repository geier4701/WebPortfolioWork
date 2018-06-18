using FloorOrderingSystem.BLL;
using FloorOrderingSystem.Workflows.Edit;
using FOS.Models;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem.Workflows
{
	public class EditExistingOrderWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();
			var listOfProducts = manager.GetAllProducts();
			IEnumerable<Tax> listOfStates = manager.GetAllStates();
			OrderLookupWorkflow lookupWorkflow = new OrderLookupWorkflow();
			Order orderToDelete = lookupWorkflow.Execute(true);

			if (orderToDelete != null)
			{
				Order orderToEdit = new Order
				{
					OrderDate = orderToDelete.OrderDate,
					OrderNumber = orderToDelete.OrderNumber,
					CustomerName = orderToDelete.CustomerName,
					State = orderToDelete.State,
					TaxRate = orderToDelete.TaxRate,
					ProductType = orderToDelete.ProductType,
					Area = orderToDelete.Area,
					CostPerSquareFoot = orderToDelete.CostPerSquareFoot,
					LaborCostPerSquareFoot = orderToDelete.LaborCostPerSquareFoot,
					MaterialCost = orderToDelete.MaterialCost,
					LaborCost = orderToDelete.LaborCost,
					TotalTax = orderToDelete.TotalTax,
					Total = orderToDelete.Total
				};

				bool shouldQuit = false;
				while (!shouldQuit)
				{
					Console.WriteLine("-----------------------------------------");
					Console.WriteLine("What would you like to edit?");
					Console.WriteLine("1. Customer Name\n2. Product Purchased\n3. State\n4. Square Footage Needed\n5. Quit");
					Console.WriteLine("-----------------------------------------");
					string fieldToEdit = Console.ReadLine();

					switch (fieldToEdit)
					{
						case "1":
							EditCustomerName.Execute(orderToEdit);
							break;

						case "2":
							EditProduct.Execute(orderToEdit, listOfProducts);
							Recalculate(orderToEdit);
							break;

						case "3":
							EditState.Execute(orderToEdit, listOfStates);
							Recalculate(orderToEdit);
							break;

						case "4":
							EditArea.Execute(orderToEdit);
							Recalculate(orderToEdit);
							break;
						case "5":
							shouldQuit = true;
							break;
						default:
							Console.WriteLine("You must enter a number to make a selection. Quit to save.");
							break;
					}
				}

				EditExistingOrderResponse response = manager.EditOrder(orderToEdit);

				if (response.Success)
				{
					ConsoleIO.DisplayOrderDetails(response.Order);
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

		public Order Recalculate(Order orderToEdit)
		{
			orderToEdit.MaterialCost = orderToEdit.Area * orderToEdit.CostPerSquareFoot;
			orderToEdit.LaborCost = orderToEdit.Area * orderToEdit.LaborCostPerSquareFoot;
			orderToEdit.TotalTax = (orderToEdit.MaterialCost + orderToEdit.LaborCost) * (orderToEdit.TaxRate / 100);
			orderToEdit.Total = orderToEdit.LaborCost + orderToEdit.MaterialCost + orderToEdit.TotalTax;

			return orderToEdit;
		}
	}
}

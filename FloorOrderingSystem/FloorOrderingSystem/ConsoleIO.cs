using FOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem
{
	public class ConsoleIO
	{
		public static void DisplayOrderDetails(Order orderToDisplay)
		{
			Console.Clear();
			Console.WriteLine("ORDER DETAILS");
			Console.WriteLine("---------------------------------");
			Console.WriteLine($"Order Date: {orderToDisplay.OrderDate}");
			Console.WriteLine($"Order Number: {orderToDisplay.OrderNumber}");
			Console.WriteLine($"Customer Name: {orderToDisplay.CustomerName}");
			Console.WriteLine($"State: {orderToDisplay.State}");
			Console.WriteLine($"Tax Rate: {orderToDisplay.TaxRate}");
			Console.WriteLine($"Product Type: {orderToDisplay.ProductType}");
			Console.WriteLine($"Area in Square Feet: {orderToDisplay.Area}");
			Console.WriteLine($"Cost per Square Foot: {orderToDisplay.CostPerSquareFoot}");
			Console.WriteLine($"Labor Cost per Square Foot: {orderToDisplay.LaborCostPerSquareFoot}");
			Console.WriteLine($"Material Cost: {Math.Round(orderToDisplay.MaterialCost, 2)}");
			Console.WriteLine($"Labor Cost: {Math.Round(orderToDisplay.LaborCost, 2)}");
			Console.WriteLine($"Total Tax: {Math.Round(orderToDisplay.TotalTax, 2)}");
			Console.WriteLine("---------------------------------");
			Console.WriteLine($"Grand Total: {Math.Round(orderToDisplay.Total, 2)}");

		}
	}
}

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
	public class PlaceNewOrderWorkflow
	{
		public void Execute()
		{
			OrderManager manager = OrderManagerFactory.Create();

			Console.Clear();
			Console.WriteLine("Enter a new order");
			Console.WriteLine("-------------------");

			DateTime dateInput = new DateTime();
			bool dateVerify = false;
			do
			{
				Console.WriteLine("Enter the order date in the format MM/DD/YYYY");
				string stringDate = Console.ReadLine();

				try
				{
					dateInput = DateTime.Parse(stringDate);
					dateVerify = true;
				}
				catch
				{
					Console.WriteLine("Incorrect format");
					Console.ReadKey();
					Console.Clear();
				}
			} while (!dateVerify);

			Order orderToAdd = new Order();

			string nameInput;
			bool validName = false;
			do
			{
				Console.Write("Enter the customer's name: ");
				nameInput = Console.ReadLine();

				validName = ValidateInput.CheckForInput(nameInput);
			} while (!validName);

			string productInput;
			bool validProduct = false;
			do
			{
				Console.Write("Enter the product material being purchased: ");
				productInput = Console.ReadLine();

				validProduct = ValidateInput.CheckForInput(productInput);
			} while (!validProduct);

			string stateInput;
			bool validState = false;
			do
			{
				Console.Write("Enter the customer's two letter state abbreviation: ");
				stateInput = Console.ReadLine().ToUpper();

				validState = ValidateInput.CheckForInput(stateInput);
			} while (!validState);

			decimal areaInput = -1.0m;
			do
			{
				Console.Write("Enter the square footage needed: ");
				string stringArea = Console.ReadLine();

				areaInput = ValidateInput.CheckForDecimal(stringArea);

				if(areaInput < 100)
				{
					Console.WriteLine("Order minimum is 100 square feet");
					areaInput = -1.0m;
				}

			} while (areaInput == -1.0m);

			PlaceNewOrderResponse response = manager.NewOrder(dateInput, nameInput, stateInput, productInput, areaInput);

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
}
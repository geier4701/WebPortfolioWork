using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Models;

namespace FloorOrderingSystem.Workflows.Edit
{
	public class EditProduct
	{
		public static void Execute(Order orderToEdit, IEnumerable<Product> allProducts)
		{
			bool validInput = false;
			do
			{
				Console.Write("Enter the new product: ");
				string newStringValue = Console.ReadLine();

				if (string.IsNullOrEmpty(newStringValue))
				{
					validInput = true;
				}
				else
				{
					Product productToChange = allProducts.SingleOrDefault(p => p.ProductType.ToLower() == newStringValue.ToLower());

					if (productToChange != null)
					{
						orderToEdit.ProductType = productToChange.ProductType;
						orderToEdit.CostPerSquareFoot = productToChange.CostPerSquareFoot;
						orderToEdit.LaborCostPerSquareFoot = productToChange.LaborCostPerSquareFoot;

						validInput = true;
					}
				}

			} while (!validInput);
		}
	}
}

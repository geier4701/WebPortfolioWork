using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Models;

namespace FloorOrderingSystem.Workflows.Edit
{
	public class EditArea
	{
		public static void Execute(Order orderToEdit)
		{
			bool validInput = false; 
			do
			{
				Console.Write("Enter the square footage of order: ");
				string newStringValue = Console.ReadLine();

				if (string.IsNullOrEmpty(newStringValue))
				{
					validInput = true;
				}
				else
				{
					decimal parsedValue = decimal.MinValue;

					if (decimal.TryParse(newStringValue, out parsedValue) && parsedValue >= 100)
					{
						validInput = true;
						orderToEdit.Area = parsedValue;
					}
					
				}

			} while (!validInput);
		}
	}
}

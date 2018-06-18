using FOS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem.Workflows.Edit
{
	public class EditState
	{
		public static void Execute(Order orderToEdit, IEnumerable<Tax> allTaxes)
		{
			bool validInput = false;
			do
			{
				Console.Write("Enter the new state: ");
				string newStringValue = Console.ReadLine();

				if (string.IsNullOrEmpty(newStringValue))
				{
					validInput = true;
				}
				else
				{
					Tax stateToChange = allTaxes.SingleOrDefault(t => t.StateAbbreviation.ToLower() == newStringValue.ToLower());

					if (stateToChange != null)
					{
						orderToEdit.State = stateToChange.StateAbbreviation;
						orderToEdit.TaxRate = stateToChange.TaxRate;
						validInput = true;
					}
				}

			} while (!validInput);
		}
	}
}

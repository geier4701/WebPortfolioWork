using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Models;

namespace FloorOrderingSystem.Workflows.Edit
{
	public class EditCustomerName
	{
		public static void Execute(Order orderToEdit)
		{
			Console.Write("Enter the customer's correct name: ");
			string newStringValue = Console.ReadLine();
			
			if (!string.IsNullOrEmpty(newStringValue))
			{
				orderToEdit.CustomerName = newStringValue;
			}
		}
	}
}

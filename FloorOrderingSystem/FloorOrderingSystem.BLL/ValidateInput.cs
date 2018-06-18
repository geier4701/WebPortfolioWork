using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem.BLL
{
	public static class ValidateInput
	{
		public static bool CheckForInput (string userInput)
		{
			bool validState = false;

			if (!string.IsNullOrEmpty(userInput))
			{
				validState = true;
			}

			return validState;
		}

		public static decimal CheckForDecimal (string userInput)
		{
			decimal decimalReturn;

			try
			{
				decimalReturn = decimal.Parse(userInput);
			}
			catch
			{
				Console.WriteLine("Please enter your number as a decimal");
				Console.ReadKey();
				decimalReturn = -1.0m;
			}

			return decimalReturn;
		}
	}
}

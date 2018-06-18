using FOS.Models;
using FOS.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Data
{
	public class TaxFileRepo : ITaxRepo
	{
		const string statePath = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data\Taxes.txt";

		public IEnumerable<Tax> GetAllStates()
		{
			List<Tax> allStates = new List<Tax>();

			using (StreamReader reader = new StreamReader(statePath))
			{
				reader.ReadLine();
				string line;

				while ((line = reader.ReadLine()) != null)
				{
					Tax taxToAdd = new Tax();
					string[] columns = line.Split(',');

					taxToAdd.StateAbbreviation = columns[0];
					taxToAdd.StateName = columns[1];
					taxToAdd.TaxRate = decimal.Parse(columns[2]);

					allStates.Add(taxToAdd);
				}
			}

			return allStates;
		}

		public Tax GetState(string inputState)
		{
			try
			{
				using (StreamReader reader = new StreamReader(statePath))
				{
					reader.ReadLine();
					string line;
					while((line = reader.ReadLine()) != null)
					{
						Tax taxToReturn = new Tax();
						string[] columns = line.Split(',');

						taxToReturn.StateAbbreviation = columns[0];
						taxToReturn.StateName = columns[1];
						taxToReturn.TaxRate = decimal.Parse(columns[2]);

						if (taxToReturn.StateAbbreviation == inputState)
						{
							return taxToReturn;
						}
					}
				}
			}
			catch
			{
				return null;
			}

			return null;
		}
	}
}

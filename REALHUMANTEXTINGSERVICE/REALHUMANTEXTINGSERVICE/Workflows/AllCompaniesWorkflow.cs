using REALHUMANTEXTINGSERVICE.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.Workflows
{
	public class AllCompaniesWorkflow
	{
		public void Execute()
		{
			var manager = ManagerFactory.Create();
			var response = manager.GetAllCompanies();

			if (response.Success)
			{
				foreach (var company in response.AllCompanies)
				{
					Console.WriteLine($"Company ID: {company.id}");
					Console.WriteLine($"Company Name: {company.company}");
					Console.WriteLine($"Company City: {company.city}");
					Console.WriteLine($"Company TimeZone: {company.timezone}");
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine(response.Message);
			}

			Console.WriteLine("Press any key to continue");
			Console.ReadKey();
		}
	}
}

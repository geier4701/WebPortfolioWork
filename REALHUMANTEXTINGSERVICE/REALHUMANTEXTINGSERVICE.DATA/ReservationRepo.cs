using Newtonsoft.Json;
using REALHUMANTEXTINGSERVICE.MODELS;
using REALHUMANTEXTINGSERVICE.MODELS.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.DATA
{
	public class ReservationRepo : IReservationRepo
	{
		const string guestPath = @"C:\Users\Poor Richard\Desktop\Software Guild\Personal Projects\REALHUMANTEXTINGSERVICE\DataFiles\Guests.json";
		const string companyPath = @"C:\Users\Poor Richard\Desktop\Software Guild\Personal Projects\REALHUMANTEXTINGSERVICE\DataFiles\Companies.json";

		public List<Guest> GetAllGuests()
		{
			var allGuests = new List<Guest>();

			using (StreamReader reader = new StreamReader(guestPath))
			{
				allGuests = JsonConvert.DeserializeObject<List<Guest>>(reader.ReadToEnd());
			}

			return allGuests;
		}

		public List<Company> GetAllCompanies()
		{
			var allCompanies = new List<Company>();

			using (StreamReader reader = new StreamReader(companyPath))
			{
				allCompanies = JsonConvert.DeserializeObject<List<Company>>(reader.ReadToEnd());
			}

			return allCompanies;
		}

		public Guest GetGuest(int guestId)
		{
			return GetAllGuests().SingleOrDefault(g => g.id == guestId);
		}

		public Company GetCompany(int companyId)
		{
			return GetAllCompanies().SingleOrDefault(c => c.id == companyId);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.MODELS.Interfaces
{
	public interface IReservationRepo
	{
		List<Guest> GetAllGuests();
		List<Company> GetAllCompanies();
		Guest GetGuest(int guestId);
		Company GetCompany(int companyId);
	}
}

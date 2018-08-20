using REALHUMANTEXTINGSERVICE.BLL;
using REALHUMANTEXTINGSERVICE.MODELS.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.Workflows
{
	public class AllGuestsWorkflow
	{
		public void Execute()
		{
			var manager = ManagerFactory.Create();
			AllGuestsResponse response = manager.GetAllGuests();

			if (response.Success)
			{
				foreach (var guest in response.AllGuests)
				{
					Console.WriteLine($"Guest Id: {guest.id}");
					Console.WriteLine($"Guest Name: {guest.firstName} {guest.lastName}");
					Console.WriteLine($"Guest Reservation:");
					Console.WriteLine($"	Room Number:	{guest.reservation.roomNumber}");
					Console.WriteLine($"	Check In:	{DateConverter.ToDateTime(guest.reservation.startTimeStamp)}");
					Console.WriteLine($"	Check Out:	{DateConverter.ToDateTime(guest.reservation.endTimeStamp)}");
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

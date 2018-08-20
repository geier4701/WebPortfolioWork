using REALHUMANTEXTINGSERVICE.DATA;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REALHUMANTEXTINGSERVICE.BLL
{
	public static class ManagerFactory
	{
		public static MessagingManager Create()
		{
			string mode = ConfigurationManager.AppSettings["Mode"].ToString();

			switch (mode)
			{
				case "prod":
					return new MessagingManager(new ReservationRepo(), new MessageRepo());
				default:
					throw new Exception("Mode value in app config is not valid");
			}
		}
	}
}

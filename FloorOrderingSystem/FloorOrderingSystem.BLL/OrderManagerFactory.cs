using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FOS.Data;

namespace FloorOrderingSystem.BLL
{
	public static class OrderManagerFactory
	{
		public static OrderManager Create()
		{
			string mode = ConfigurationManager.AppSettings["Mode"].ToString();

			switch (mode)
			{
				case "prod":
					return new OrderManager(new OrderFileRepo(), new ProductFileRepo(), new TaxFileRepo());
				default:
					throw new Exception("Mode value in app config is not valid");
			}
		}
	}
}

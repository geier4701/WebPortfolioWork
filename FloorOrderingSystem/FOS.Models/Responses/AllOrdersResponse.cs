using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Responses
{
	public class AllOrdersResponse : Response
	{
		public List<Order> ListOfOrders { get; set; }
	}
}

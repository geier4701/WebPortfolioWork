using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Models.Interfaces
{
	public interface IOrderRepo
	{
		Order LoadOrder(int orderNumber, DateTime orderDate);
		Order AddOrder(Order orderToAdd);
		Order EditOrder(Order orderToEdit);
		void DeleteOrder(Order orderToDelete);
		List<Order> LoadAllOrders(DateTime inputOrderDate);
	}
}

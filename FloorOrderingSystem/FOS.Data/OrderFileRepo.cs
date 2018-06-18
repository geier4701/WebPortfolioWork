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
	public class OrderFileRepo : IOrderRepo
	{
		public Order LoadOrder(int orderNumber, DateTime orderDate)
		{
			string path = FindFile(orderDate);

			if (path != "notFound")
			{
				try
				{
					using (StreamReader reader = new StreamReader(path))
					{
						reader.ReadLine();
						string line;
						while ((line = reader.ReadLine()) != null)
						{
							Order _order = new Order();
							string[] columns = line.Split(',');

							_order.OrderNumber = int.Parse(columns[0]);
							_order.CustomerName = columns[1];
							_order.State = columns[2];
							_order.TaxRate = decimal.Parse(columns[3]);
							_order.ProductType = columns[4];
							_order.Area = decimal.Parse(columns[5]);
							_order.CostPerSquareFoot = decimal.Parse(columns[6]);
							_order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
							_order.MaterialCost = decimal.Parse(columns[8]);
							_order.LaborCost = decimal.Parse(columns[9]);
							_order.TotalTax = decimal.Parse(columns[10]);
							_order.Total = decimal.Parse(columns[11]);

							if (_order.OrderNumber == orderNumber)
							{
								_order.OrderDate = orderDate;
								return _order;
							}
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("An error occurred at: " + ex.StackTrace);
					Console.ReadKey();
					return null;
				}
			}

			return null;
		}

		public Order AddOrder(Order orderToAdd)
		{
			try
			{
				string path = FindFile(orderToAdd.OrderDate);
				if (path == "notFound")
				{
					path = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data\Orders_" + orderToAdd.OrderDate.Date.ToString("MMddyyyy") + ".txt";
					
					using (StreamWriter writer = File.AppendText(path))
					{
						writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
					}

					orderToAdd.OrderNumber = 1;
				}
				else
				{
					using (StreamReader reader = new StreamReader(path))
					{
						reader.ReadLine();
						string line;
						int lastOrder = 0;
						while ((line = reader.ReadLine()) != null)
						{
							string[] columns = line.Split(',');
							int orderNumber = int.Parse(columns[0]);
							if (orderNumber > lastOrder)
							{
								lastOrder = orderNumber;
							}
						}

						orderToAdd.OrderNumber = lastOrder + 1;
					}
				}

				using (StreamWriter writer = File.AppendText(path))
				{
					writer.WriteLine($"{orderToAdd.OrderNumber},{orderToAdd.CustomerName},{orderToAdd.State},{orderToAdd.TaxRate},{orderToAdd.ProductType}," +
						$"{orderToAdd.Area},{orderToAdd.CostPerSquareFoot},{orderToAdd.LaborCostPerSquareFoot},{orderToAdd.MaterialCost},{orderToAdd.LaborCost}," +
						$"{orderToAdd.TotalTax},{orderToAdd.Total}");
				}

				return orderToAdd;
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred at: " + ex.StackTrace);
				Console.ReadKey();
				return null;
			}
		}

		public Order EditOrder(Order orderToEdit)
		{
			string path = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data\Orders_" + orderToEdit.OrderDate.Date.ToString("MMddyyyy") + ".txt";

			try
			{
				using (StreamWriter writer = File.AppendText(path))
				{
					writer.WriteLine($"{orderToEdit.OrderNumber},{orderToEdit.CustomerName},{orderToEdit.State},{orderToEdit.TaxRate},{orderToEdit.ProductType}," +
						$"{orderToEdit.Area},{orderToEdit.CostPerSquareFoot},{orderToEdit.LaborCostPerSquareFoot},{orderToEdit.MaterialCost},{orderToEdit.LaborCost}," +
						$"{orderToEdit.TotalTax},{orderToEdit.Total}");
				}

				Order checkSave = LoadOrder(orderToEdit.OrderNumber, orderToEdit.OrderDate);

				return checkSave;
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred at: " + ex.StackTrace);
				Console.ReadKey();
				return null;
			}
		}

		public void DeleteOrder(Order orderToDelete)
		{
			string tempData = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\TempData\Orders_" + orderToDelete.OrderDate.Date.ToString("MMddyyyy") + ".txt";
			string Data = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data\Orders_" + orderToDelete.OrderDate.Date.ToString("MMddyyyy") + ".txt";

			string path = FindFile(orderToDelete.OrderDate);

			using (StreamReader reader = new StreamReader(path))
			using (StreamWriter writer = new StreamWriter(tempData))
			{
				reader.ReadLine();
				writer.WriteLine("OrderNumber,CustomerName,State,TaxRate,ProductType,Area,CostPerSquareFoot,LaborCostPerSquareFoot,MaterialCost,LaborCost,Tax,Total");
				string line;

				while((line = reader.ReadLine()) != null)
				{
					string[] columns = line.Split(',');

					if (int.Parse(columns[0]) != orderToDelete.OrderNumber)
					{
						writer.WriteLine(line);
					}
				}
			}

			File.Copy(tempData, Data, true);

			File.Delete(tempData);
		}

		public string FindFile(DateTime orderDate)
		{
			string path = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data";
			DirectoryInfo dataFile = new DirectoryInfo(@"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data");
			IEnumerable<FileInfo> listOfFiles = dataFile.EnumerateFiles();

			bool found = false;

			string fileName = "Orders_" + orderDate.Date.ToString("MMddyyyy") + ".txt";

			foreach (var file in listOfFiles)
			{
				if (file.Name.ToString() == fileName)
				{
					path = Path.Combine(path, fileName);
					return path;
				}
			}

			if (!found)
			{
				path = "notFound";
			}

			return path;
		}

		public List<Order> LoadAllOrders(DateTime inputOrderDate)
		{
			string path = FindFile(inputOrderDate);
			List<Order> ListOfOrders = new List<Order>();

			try
			{
				using (StreamReader reader = new StreamReader(path))
				{
					reader.ReadLine();
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						Order _order = new Order();
						string[] columns = line.Split(',');

						_order.OrderNumber = int.Parse(columns[0]);
						_order.CustomerName = columns[1];
						_order.State = columns[2];
						_order.TaxRate = decimal.Parse(columns[3]);
						_order.ProductType = columns[4];
						_order.Area = decimal.Parse(columns[5]);
						_order.CostPerSquareFoot = decimal.Parse(columns[6]);
						_order.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
						_order.MaterialCost = decimal.Parse(columns[8]);
						_order.LaborCost = decimal.Parse(columns[9]);
						_order.TotalTax = decimal.Parse(columns[10]);
						_order.Total = decimal.Parse(columns[11]);

						ListOfOrders.Add(_order);
					}
				}

				return ListOfOrders;
			}
			catch (Exception ex)
			{
				Console.WriteLine("An error occurred at: " + ex.StackTrace);
				Console.ReadKey();
				return null;
			}
		}
	}
}

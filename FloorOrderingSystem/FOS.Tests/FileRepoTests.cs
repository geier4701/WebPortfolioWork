using FloorOrderingSystem.BLL;
using FOS.Models;
using FOS.Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Tests
{
	[TestFixture]
	public class FileRepoTests
	{
		const string seed = @"C:\Users\Poor Richard\Desktop\SeedFolder\Orders_06012013.txt";
		const string dest = @"C:\Users\Poor Richard\Desktop\DestinationFolder\Orders_06012013.txt";
		const string edit = @"C:\Users\Poor Richard\Desktop\DestinationFolder\Edited.txt";

		[SetUp]
		public void Setup()
		{
			if (File.Exists(dest))
			{
				File.Delete(dest);
			}

			if (File.Exists(edit))
			{
				File.Delete(edit);
			}

			File.Copy(seed, dest);
		}

		[TestCase (1, 2013, 06, 01, true)]
		[TestCase (10, 2013, 06, 01, false)] //Bad order number
		[TestCase (1, 2013, 01, 01, false)] //Bad order date
		public void CanLoadCorrectOrder(int orderNumber, int year, int month, int day, bool success)
		{
			DateTime orderDate = new DateTime(year, month, day);

			OrderManager manager = OrderManagerFactory.Create();

			OrderLookupResponse response = manager.OrderLookup(orderNumber, orderDate);

			Assert.AreEqual(success, response.Success);
		}

		[TestCase ("12/12/1212", "John", "PA", "Wood", 100, true)]
		//Manager does not validate inputs for placing new order
		public void CanAddNew(string OrderDate, string NameInput, string StateInput, string ProductInput, decimal AreaInput, bool success)
		{
			OrderManager manager = OrderManagerFactory.Create();
			DateTime orderDate = DateTime.Parse(OrderDate);

			PlaceNewOrderResponse response = manager.NewOrder(orderDate, NameInput, StateInput, ProductInput, AreaInput);
			OrderLookupResponse responseLookup = manager.OrderLookup(response.Order.OrderNumber, orderDate);

			Assert.AreEqual(success, response.Success);
			Assert.AreEqual(responseLookup.Order.CustomerName, NameInput);
			Assert.AreEqual(responseLookup.Order.State, StateInput);
			Assert.AreEqual(responseLookup.Order.ProductType, ProductInput);
			Assert.AreEqual(responseLookup.Order.Area, AreaInput);
		}

		[TestCase("12/12/1212", "Angie", "OH", "Laminate", 200, true)]
		public void CanDelete(string OrderDate, string NameInput, string StateInput, string ProductInput, decimal AreaInput, bool success)
		{
			OrderManager manager = OrderManagerFactory.Create();
			DateTime orderDate = DateTime.Parse(OrderDate);

			PlaceNewOrderResponse response = manager.NewOrder(orderDate, NameInput, StateInput, ProductInput, AreaInput);

			DeleteOrderResponse responseDelete = manager.DeleteOrder(response.Order);

			Assert.AreEqual(success, response.Success);
		}

		[TestCase("12/12/1212", "John", "PA", "Wood", 100, "Richard", "OH", "Carpet", 200, true)]
		public void CanEdit(string OrderDate, string NameInput, string StateInput, string ProductInput, decimal AreaInput, string NameEdit, string StateEdit, string ProductEdit, decimal AreaEdit, bool success)
		{
			OrderManager manager = OrderManagerFactory.Create();
			DateTime orderDate = DateTime.Parse(OrderDate);

			PlaceNewOrderResponse response = manager.NewOrder(orderDate, NameInput, StateInput, ProductInput, AreaInput);
			PlaceNewOrderResponse responseEdit = manager.NewOrder(orderDate, NameEdit, StateEdit, ProductEdit, AreaEdit);
			
			EditExistingOrderResponse editResponse = manager.EditOrder(responseEdit.Order);

			Assert.AreEqual(success, response.Success);
			Assert.AreEqual(editResponse.Order.CustomerName, NameEdit);
			Assert.AreEqual(editResponse.Order.State, StateEdit);
			Assert.AreEqual(editResponse.Order.ProductType, ProductEdit);
			Assert.AreEqual(editResponse.Order.Area, AreaEdit);
		}
	}
}

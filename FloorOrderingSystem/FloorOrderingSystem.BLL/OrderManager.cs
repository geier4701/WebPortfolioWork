using FOS.Models;
using FOS.Models.Interfaces;
using FOS.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloorOrderingSystem.BLL
{
	public class OrderManager
	{
		private IOrderRepo _orderRepo;
		private IProductRepo _productRepo;
		private ITaxRepo _taxRepo;

		public OrderManager(IOrderRepo orderRepo, IProductRepo productRepo, ITaxRepo taxRepo)
		{
			_orderRepo = orderRepo;
			_productRepo = productRepo;
			_taxRepo = taxRepo;
		}

		public IEnumerable<Tax> GetAllStates()
		{
			return _taxRepo.GetAllStates();
		}

		public IEnumerable<Product> GetAllProducts ()
		{
			return _productRepo.GetAllProducts();
		}

		public OrderLookupResponse OrderLookup (int orderNumber, DateTime orderDate)
		{
			OrderLookupResponse response = new OrderLookupResponse
			{
				Order = _orderRepo.LoadOrder(orderNumber, orderDate)
			};

			if (response.Order == null)
			{
				response.Success = false;
				response.Message = $"{orderNumber} is not a valid order number for that date";
			}
			else response.Success = true;

			return response;
		}

		public AllOrdersResponse AllOrderLookup(DateTime inputOrderDate)
		{
			AllOrdersResponse response = new AllOrdersResponse
			{
				ListOfOrders = _orderRepo.LoadAllOrders(inputOrderDate)
			};

			if (response.ListOfOrders == null)
			{
				response.Success = false;
				response.Message = $"There are no orders for {inputOrderDate}";
			}
			else response.Success = true;

			return response;
		}

		public PlaceNewOrderResponse NewOrder (DateTime DateInput, string NameInput, string StateInput, string ProductInput, decimal AreaInput)
		{
			PlaceNewOrderResponse response = new PlaceNewOrderResponse();

			Order orderToAdd = new Order
			{
				OrderDate = DateInput,
				CustomerName = NameInput
			};

			string stateInput = StateInput;
			string productInput = ProductInput;
			orderToAdd.Area = AreaInput;

			bool failState = false;

			Product product = _productRepo.GetProduct(ProductInput);
			if (product == null)
			{
				response.Message = $"{ProductInput} is not a valid product";
				failState = true;
			}
			else
			{
				orderToAdd.ProductType = product.ProductType;
				orderToAdd.CostPerSquareFoot = product.CostPerSquareFoot;
				orderToAdd.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
			}

			Tax taxInfo = null;
			if (!failState)
			{
				taxInfo = _taxRepo.GetState(StateInput);
				if (taxInfo == null)
				{
					response.Message = $"{StateInput} is not a valid state";
					failState = true;
				}
				else
				{
					orderToAdd.State = taxInfo.StateAbbreviation;
					orderToAdd.TaxRate = taxInfo.TaxRate;
				}
			}

			if (!failState)
			{
				orderToAdd.CostPerSquareFoot = product.CostPerSquareFoot;
				orderToAdd.LaborCostPerSquareFoot = product.LaborCostPerSquareFoot;
				orderToAdd.TaxRate = taxInfo.TaxRate;
			}

			if (!failState)
			{
				orderToAdd.MaterialCost = orderToAdd.Area * orderToAdd.CostPerSquareFoot;
				orderToAdd.LaborCost = orderToAdd.Area * orderToAdd.LaborCostPerSquareFoot;
				orderToAdd.TotalTax = (orderToAdd.MaterialCost + orderToAdd.LaborCost) * (orderToAdd.TaxRate / 100);
				orderToAdd.Total = orderToAdd.MaterialCost + orderToAdd.LaborCost + orderToAdd.TotalTax;
			}

			if (!failState)
			{
				response.Order = _orderRepo.AddOrder(orderToAdd);
			}
			
			if (failState)
			{
				response.Success = false;
			}
			else if (response.Order == null)
			{
				response.Success = false;
				response.Message = "An error occurred while submitting the order";
			}
			else response.Success = true;

			return response;
		}

		public EditExistingOrderResponse EditOrder(Order editedOrder)
		{
			EditExistingOrderResponse response = new EditExistingOrderResponse();

			bool failState = false;

			if (!failState)
			{
				DeleteOrderResponse deleteResponse = DeleteOrder(editedOrder.OrderDate, editedOrder.OrderNumber);

				if (deleteResponse.Success)
				{
					response.Order = _orderRepo.EditOrder(editedOrder);
				}
				else
				{
					failState = true;
					response.Message = "Failed to delete previous order";
				}
			}

			if (failState)
			{
				response.Success = false;
			}
			else if (response.Order == null)
			{
				response.Success = false;
				response.Message = "An error occurred while submitting the order";
			}
			else response.Success = true;

			return response;
		}

		public DeleteOrderResponse DeleteOrder (Order orderToDelete)
		{
			DeleteOrderResponse response = new DeleteOrderResponse();

			_orderRepo.DeleteOrder(orderToDelete);

			OrderLookupResponse deleteCheck = OrderLookup(orderToDelete.OrderNumber, orderToDelete.OrderDate);

			if (!deleteCheck.Success)
			{
				response.Success = true;
			}
			else
			{
				response.Success = false;
				response.Message = "The order was not successfully deleted";
			}

			return response;
		}

		public DeleteOrderResponse DeleteOrder (DateTime orderDate, int orderNumber)
		{
			return DeleteOrder(OrderLookup(orderNumber, orderDate).Order);
		}
	}
}

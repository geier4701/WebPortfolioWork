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
	public class ProductFileRepo : IProductRepo
	{
		const string productPath = @"C:\Users\Poor Richard\Desktop\Software Guild\andrew-geier-individual-work\FloorOrderingSystem\Data\Products.txt";

		public IEnumerable<Product> GetAllProducts()
		{
			List<Product> allProducts = new List<Product>();

			using (StreamReader reader = new StreamReader(productPath))
			{
				reader.ReadLine();
				string line;
				while ((line = reader.ReadLine()) != null)
				{
					Product productToAdd = new Product();
					string[] columns = line.Split(',');

					productToAdd.ProductType = columns[0];
					productToAdd.CostPerSquareFoot = decimal.Parse(columns[1]);
					productToAdd.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

					allProducts.Add(productToAdd);
				}
			}

			return allProducts;
		}

		public Product GetProduct(string inputProduct)
		{
			try
			{
				using (StreamReader reader = new StreamReader(productPath))
				{
					reader.ReadLine();
					string line;
					while ((line = reader.ReadLine()) != null)
					{
						Product productToReturn = new Product();
						string[] columns = line.Split(',');

						productToReturn.ProductType = columns[0];
						productToReturn.CostPerSquareFoot = decimal.Parse(columns[1]);
						productToReturn.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

						if (productToReturn.ProductType == inputProduct)
						{
							return productToReturn;
						}
					}
				}
			}
			catch
			{
				return null;
			}

			return null;
		}
	}
}

namespace MedBilling.Models
{
	public class ProductsRepository
	{
		private static List<Product> _products = new List<Product>()
		{
			new Product { ProductId = 1, CategoryId = 1, Name = "Rzoler DSR", Quantity = 100,UnitPrice = 11.9, Price = 119 },
			new Product { ProductId = 2, CategoryId = 1, Name = "Cefomil-O", Quantity = 200,UnitPrice = 20, Price = 200 },
			new Product { ProductId = 3, CategoryId = 2, Name = "Sucram-O Syp", Quantity = 300,UnitPrice = 252, Price = 252 },
			new Product { ProductId = 4, CategoryId = 2, Name = "Laxiforce Syp", Quantity = 300,UnitPrice = 193, Price = 193 },
			new Product { ProductId = 5, CategoryId = 3, Name = "Razo", Quantity = 300,UnitPrice = 100, Price = 100 }
		};

		public static void AddProduct(Product product)
		{
			if(_products != null && _products.Count >0)
			{
                var maxId = _products.Max(x => x.ProductId);
                product.ProductId = maxId + 1;
            }
			else
			{
				product.ProductId = 1;
			}

			if (_products == null) _products = new List<Product>();
			_products.Add(product);
		}

		public static List<Product> GetProducts(bool loadCategory = false)   //=> _products;
		{
			if (!loadCategory)
			{
				return _products;
			}
			else
			{
				if (_products != null && _products.Count > 0)
				{
					_products.ForEach(x =>
					{
						if (x.CategoryId.HasValue)
							x.Category = CategoriesRepository.GetCategoryById(x.CategoryId.Value);
					});
				}
			}
			return _products ?? new List<Product>();
		}

		public static Product? GetProductById(int productId, bool loadCategory = false)
		{
			var product = _products.FirstOrDefault(x => x.ProductId == productId);
			if (product != null)
			{
				var prod = new Product
				{
					ProductId = product.ProductId,
					Name = product.Name,
					Quantity = product.Quantity,
					UnitPrice = product.UnitPrice,
					Price = product.Price,
					CategoryId = product.CategoryId
				};

				if (loadCategory && prod.CategoryId.HasValue) 
				{
					prod.Category = CategoriesRepository.GetCategoryById(prod.CategoryId.Value);
				}
				return prod;
			}

			return null;
		}

		public static void UpdateProduct(int productId, Product product)
		{
			if (productId != product.ProductId) return;

			var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
			if (productToUpdate != null)
			{
				productToUpdate.Name = product.Name;
				productToUpdate.Quantity = product.Quantity;
				productToUpdate.UnitPrice = product.UnitPrice;
				productToUpdate.Price = product.Price;
				productToUpdate.CategoryId = product.CategoryId;
			}
		}

		public static void DeleteProduct(int productId)
		{
			var product = _products.FirstOrDefault(x => x.ProductId == productId);
			if (product != null)
			{
				_products.Remove(product);
			}
		}

		public static List<Product> GetProductsByCategoryId(int categoryId)
		{
			var products = _products.Where(x => x.CategoryId == categoryId);
			if(products != null)
				return products.ToList();
			else
				return new List<Product>();
		}

	}
}

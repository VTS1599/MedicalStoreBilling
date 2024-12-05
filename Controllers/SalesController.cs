//using MedBilling.Models;
using CoreBusiness;
using MedBilling.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using UseCases.CategoriesUSeCases;
using UseCases.ProductsUseCases;
using UseCases;
using Microsoft.AspNetCore.Authorization;

namespace MedBilling.Controllers
{
	[Authorize(Policy = "Cashiers")]   //Only cashiers can access the sales page
	public class SalesController : Controller
    {

		private readonly IViewCategoriesUseCase viewCategoriesUseCase;
		private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
		private readonly ISellProductUseCase sellProductUseCase;
		private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;

		public SalesController(IViewCategoriesUseCase viewCategoriesUseCase,
			IViewSelectedProductUseCase viewSelectedProductUseCase,
			ISellProductUseCase sellProductUseCase,
			IViewProductsInCategoryUseCase viewProductsInCategoryUseCase)
		{
			this.viewCategoriesUseCase = viewCategoriesUseCase;
			this.viewSelectedProductUseCase = viewSelectedProductUseCase;
			this.sellProductUseCase = sellProductUseCase;
			this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
		}

		public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
				//Categories = CategoriesRepository.GetCategories()
				Categories = viewCategoriesUseCase.Execute()
			};

			return View(salesViewModel);
        }

        public IActionResult SellProductPartial(int productId)
        {
			//var product = ProductsRepository.GetProductById(productId);
			var product = viewSelectedProductUseCase.Execute(productId);
			return PartialView("_SellProduct",product);
        }

        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid) 
            {

				//Code to sell the product/ Transactions Record

				//           var prod = ProductsRepository.GetProductById(salesViewModel.SelectedProductId);
				//           if (prod != null) 
				//           {
				//TransactionRepository.Add(
				//                   "Cashier1",
				//                   //patientName,
				//                   salesViewModel.SelectedProductId,
				//                   prod.Name,
				//                   prod.Price.HasValue?prod.Price.Value:0,
				//                   prod.Quantity.HasValue?prod.Quantity.Value:0,
				//                   salesViewModel.QuantityToSell);

				//               prod.Quantity -= salesViewModel.QuantityToSell;
				//               ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, prod);
				//           }

				// Sell the product
				sellProductUseCase.Execute(
					"cashier1",
					salesViewModel.SelectedProductId,
					salesViewModel.QuantityToSell);
			}

			//var product = ProductsRepository.GetProductById(salesViewModel.SelectedProductId);
			//salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
			//salesViewModel.Categories = CategoriesRepository.GetCategories();
			
			var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);
			salesViewModel.SelectedCategoryId = (product?.CategoryId == null) ? 0 : product.CategoryId.Value;
			salesViewModel.Categories = viewCategoriesUseCase.Execute();
			
			return View("Index",salesViewModel);
        }

		//To connect with the sales page through _Products partial View
		public IActionResult ProductsByCategoryPartial(int categoryId)
		{
			//var products = ProductsRepository.GetProductsByCategoryId(categoryId);
			var products = viewProductsInCategoryUseCase.Execute(categoryId);

			return PartialView("_Products", products);
			//return View(); 
		}

	}
}

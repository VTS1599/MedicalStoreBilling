//using MedBilling.Models;
using CoreBusiness;
using MedBilling.ViewModels;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUSeCases;
using UseCases;
using Microsoft.AspNetCore.Authorization;

namespace MedBilling.Controllers
{
	[Authorize(Policy = "Inventory")]
	public class ProductsController : Controller
	{
		private readonly IAddProductUseCase addProductUseCase;
		private readonly IEditProductUseCase editProductUseCase;
		private readonly IDeleteProductUseCase deleteProductUseCase;
		private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;
		private readonly IViewProductsUseCase viewProductsUseCase;
		private readonly IViewCategoriesUseCase viewCategoriesUseCase;

		public ProductsController(
			IAddProductUseCase addProductUseCase,
			IEditProductUseCase editProductUseCase,
			IDeleteProductUseCase deleteProductUseCase,
			IViewSelectedProductUseCase viewSelectedProductUseCase,
			IViewProductsUseCase viewProductsUseCase,
			IViewCategoriesUseCase viewCategoriesUseCase)
		{
			this.addProductUseCase = addProductUseCase;
			this.editProductUseCase = editProductUseCase;
			this.deleteProductUseCase = deleteProductUseCase;
			this.viewSelectedProductUseCase = viewSelectedProductUseCase;
			this.viewProductsUseCase = viewProductsUseCase;
			this.viewCategoriesUseCase = viewCategoriesUseCase;
		}

		public IActionResult Index()
		{
            //var products = ProductsRepository.GetProducts(loadCategory: true);
			var products = viewProductsUseCase.Execute(loadCategory: true);

			return View(products);
		}

        public IActionResult Edit(int id)
        {

            ViewBag.Action = "edit";
			//var products = ProductsRepository.GetProductById(id.HasValue ? id.Value : 0);
			//return View(products);
			var selectedProduct = viewSelectedProductUseCase.Execute(id);

			var productViewModel = new ProductViewModel
            {
				//Product = ProductsRepository.GetProductById(id) ?? new Product(),
				//Categories = CategoriesRepository.GetCategories()

				Product = viewSelectedProductUseCase.Execute(id) ?? new Product(),
				Categories = viewCategoriesUseCase.Execute()
			};
            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
				//ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
				editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);
				return RedirectToAction(nameof(Index));
            }
			ViewBag.Action = "edit";
			//productViewModel.Categories = CategoriesRepository.GetCategories();
			productViewModel.Categories = viewCategoriesUseCase.Execute();

			return View(productViewModel);
        }



        public IActionResult Add()
        {
            ViewBag.Action = "add";

            var productViewModel = new ProductViewModel
            {
                //Categories = CategoriesRepository.GetCategories()
                Categories = viewCategoriesUseCase.Execute()
			}; 

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
				//ProductsRepository.AddProduct(productViewModel.Product);
				addProductUseCase.Execute(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }
			ViewBag.Action = "add";
			//productViewModel.Categories = CategoriesRepository.GetCategories();
			productViewModel.Categories = viewCategoriesUseCase.Execute(); 
            return View(productViewModel);
        }

        public IActionResult Delete(int productid)
        {
			//ProductsRepository.DeleteProduct(productid);
			deleteProductUseCase.Execute(productid);
			return RedirectToAction(nameof(Index));
        }

        //To connect with the sales page through _Products partial View
        //public IActionResult ProductsByCategoryPartial(int categoryId)
        //{  
        //    var products = ProductsRepository.GetProductsByCategoryId(categoryId);
            
        //    return PartialView("_Products", products);
        //    //return View(); 
        //}

    }
}

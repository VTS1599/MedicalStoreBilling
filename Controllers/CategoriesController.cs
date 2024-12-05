using CoreBusiness;
using Microsoft.AspNetCore.Authorization;

//using MedBilling.Models;
using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.CategoriesUSeCases;

namespace MedBilling.Controllers
{
    [Authorize(Policy = "Inventory")]
    public class CategoriesController : Controller
    {
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;

        public CategoriesController(
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IAddCategoryUseCase addCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase)
        {
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedCategoryUseCase = viewSelectedCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.addCategoryUseCase = addCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
        }
        public IActionResult Index()
        {
            //var categories = CategoriesRepository.GetCategories();
            var categories = viewCategoriesUseCase.Execute();
            return View(categories);
        }
        //By default [HttpGet] method
        public IActionResult Edit(int? id) 
        {
            //if (id.HasValue)
            //    return new ContentResult { Content = id.ToString() };
            //else
            //    return new ContentResult { Content = "null content" };

            //var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };

            ViewBag.Action = "edit";

            //var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);

            var category = viewSelectedCategoryUseCase.Execute(id.HasValue ? id.Value : 0);

            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                //CategoriesRepository.UpdateCategory(category.CategoryId, category);
                editCategoryUseCase.Execute(category.CategoryId, category);
                return RedirectToAction(nameof(Index));
            }
			ViewBag.Action = "edit";
			return View(category);
        }



        public IActionResult Add() 
        {
            ViewBag.Action = "add";

            return View(); 
        }

        [HttpPost]
		public IActionResult Add(Category category)
		{
            if (ModelState.IsValid) 
            {
                //CategoriesRepository.AddCategory(category);
                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }
			ViewBag.Action = "add";
			return View(category);
		}

        public IActionResult Delete(int categoryId)
        {
            //CategoriesRepository.DeleteCategory(categoryId);
            deleteCategoryUseCase.Execute(categoryId);
            return RedirectToAction(nameof(Index));
        }
	}
}

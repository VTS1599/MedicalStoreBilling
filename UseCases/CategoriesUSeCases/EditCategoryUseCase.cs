using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUSeCases
{
    public class EditCategoryUseCase : IEditCategoryUseCase
    {
        private readonly ICategoryRepository categoryRepository;

        public EditCategoryUseCase(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Execute(int categoryId, Category category)
        {
            categoryRepository.UpdateCategory(categoryId, category);
        }
    }
}

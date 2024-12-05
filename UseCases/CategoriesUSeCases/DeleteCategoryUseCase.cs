using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.CategoriesUseCases;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUSeCases
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoryRepository categoryRepository;

        public DeleteCategoryUseCase(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public void Execute(int categoryId)
        {
            categoryRepository.DeleteCategory(categoryId);
        }
    }
}

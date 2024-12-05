using CoreBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class CategoriesInMemoryRepository : ICategoryRepository
    {
        private List<Category> _categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Tablets", Description = "Tablets"},
            new Category { CategoryId = 2, Name = "Syrups", Description = "Syrups"},
            new Category { CategoryId = 3, Name = "Injections", Description = "Injections"}
        };
        public void AddCategory(Category category)
        {
            if (_categories != null && _categories.Count > 0)
            {
                var maxId = _categories.Max(x => x.CategoryId);
                category.CategoryId = maxId + 1;
            }
            else
            {
                category.CategoryId = 1;
            }
            if (_categories == null) _categories = new List<Category>();
            _categories.Add(category);
        }

        public IEnumerable<Category> GetCategories() => _categories;

        public Category? GetCategoryById(int Categoryid)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == Categoryid);
            if (category != null)
            {
                return new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                };
            }
            return null;
        }

        public void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;

            var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}

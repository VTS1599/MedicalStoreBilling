using CoreBusiness;

namespace UseCases.CategoriesUSeCases
{
    public interface IViewCategoriesUseCase
    {
        IEnumerable<Category> Execute();
    }
}
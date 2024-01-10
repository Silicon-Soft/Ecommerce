using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICategoryService
    {
        List<ViewCategoryVM> GetCategories();
        void AddCategory(CategoryVM categoryVM);

        CategoryVM GetCategory(int id);
    }
}

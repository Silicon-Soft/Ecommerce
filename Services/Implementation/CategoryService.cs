using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class CategoryService:ICategoryService
    {
        private readonly IGenericReopsitory<Category> _genericReopsitory;
        private IMapper _mapper;
        public CategoryService(IGenericReopsitory<Category> genericReopsitory,IMapper mapper )
        {
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public List<ViewCategoryVM> GetCategories()
        {
            try
            {
                List<Category> Categories = _genericReopsitory.GetAll();

                List<ViewCategoryVM> viewCategoryVMs = _mapper.Map<List<ViewCategoryVM>>(Categories);

                return viewCategoryVMs;
            }
            catch (Exception)
            {
                throw;
            }
            
        }
        public void AddCategory(CategoryVM categoryVM)
        {
            Category category= _mapper.Map<Category>(categoryVM);
            _genericReopsitory.Add(category);
        }
        public CategoryVM GetCategory(int id)
        {
            Category category=_genericReopsitory.GetById(id);
            CategoryVM categoryVM=_mapper.Map<CategoryVM>(category);
            return categoryVM;
        }

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.ViewModel
{
    public class HomeViewModel
    {
        public string SelectedCountry { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
        
    }
}

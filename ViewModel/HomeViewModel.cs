using Microsoft.AspNetCore.Mvc.Rendering;


    public class HomeViewModel
    {
        public string SelectedCountry { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
        
    }


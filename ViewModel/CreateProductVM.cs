using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class CreateProductVM
    {
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }
        public string imagelink { get; set; }
        [Required]
        public IFormFile imagefile { get; set; }
        
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public string SelectedCategory { get; set; }
        public List<SelectListItem> CategoriesSelectList { get; set; }
    }
}

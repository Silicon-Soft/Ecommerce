using System.ComponentModel.DataAnnotations;
namespace Ecommerce.ViewModel
{
    public class CategoryVM
    {
        [Required]
        public string CategoryName { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class ViewCategoryVM
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

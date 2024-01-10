using Ecommerce.Models;

namespace Ecommerce.ViewModel
{
    public class SerachProductVM
    {
        public IQueryable<Product> Products { get; set; }
    }
}

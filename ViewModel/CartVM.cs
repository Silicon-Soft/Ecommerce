using Ecommerce.Models;

namespace Ecommerce.ViewModel
{
    public class CartVM
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
        public List<Product> products { get; set; }

    }
}

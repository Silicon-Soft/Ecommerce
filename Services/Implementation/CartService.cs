using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericReopsitory<Cart> _genericReopsitory;
        public CartService(IGenericReopsitory<Cart> genericReopsitory)
        {
            _genericReopsitory = genericReopsitory;
        }
        public CartVM createCart()
        {
            CartVM cartVM = new();
            return cartVM;
        }
        public bool IsCartEmpty(string id)
        {

            return true;
        }
    }
}

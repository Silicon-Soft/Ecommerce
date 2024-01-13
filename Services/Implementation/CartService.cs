using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;

namespace Ecommerce.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericReopsitory<Cart> _genericReopsitory;
        public CartService(IGenericReopsitory<Cart> genericReopsitory)
        {
            _genericReopsitory = genericReopsitory;
        }
        
        public bool IsCartEmpty(string userId)
        {
            IEnumerable<Cart> carts = _genericReopsitory.GetDatas().Where(x => x.UserId == userId);
            if (carts == null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}



using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICartService
    {
        
        bool IsCartEmpty(string id);
        CreatecartVM Createcart(CreatecartVM createcartVM);
        ReadCartVM ReadCart(string id);
        ReadCartVM UpdateCart(CreatecartVM createcartVM);
    }
}

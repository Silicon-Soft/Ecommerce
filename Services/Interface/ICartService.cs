using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICartService
    {
        CartVM createCart();
        bool IsCartEmpty(string id);
    }
}

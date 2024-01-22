using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<ViewOrderVM>> GetAllOrder();
        CreateOrderVM CreateOrder(CreateOrderVM createOrderVM);
        ViewOrderVM GetOrderById(int id);
        CreateOrderVM InitiateOrder(String userid, string customerNumber, string shippingid);
    }
}

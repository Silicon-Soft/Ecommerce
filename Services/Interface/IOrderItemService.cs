using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface IOrderItemService
    {
        List<ViewOrderitemVM> GetALLOrder_item(string userid);
        List<CreateOrderItemVM> CreateListofOrders(List<ViewCart_itemVM> viewCart_ItemVMs, CreateOrderVM createOrderVM);
        CreateOrderItemVM CreateOrderitem(CreateOrderItemVM createOrderItemVM);
    }
}

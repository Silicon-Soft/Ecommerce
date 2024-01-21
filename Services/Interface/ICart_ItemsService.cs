using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICart_ItemsService
    {
        CreateCart_itemVM CreateCart_item(CreateCart_itemVM createCart_ItemVM);
        int getCartItemQuantity(string userid);
        List<ViewCart_itemVM> GetCart_item(string userid);
        int getCartitemid(int cartid, string userid, int productid);
        CreateCart_itemVM updateCart_item(CreateCart_itemVM createCart_ItemVM);
        CreateCart_itemVM GetCreateCart_ItembyID(int id);
        void DeleteCart_item(int id);
        void DeleteListofCart_item(List<ViewCart_itemVM> viewCart_ItemVMs);
    }
}



using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICartService
    {
        
        bool IsCartEmpty(string id);
        CreatecartVM Createcart(CreatecartVM createcartVM);
        ReadCartVM ReadCart(string id);
        ReadCartVM UpdateCart(CreatecartVM createcartVM);
        bool IsproductIncart(int productId,string userid);

        int GetQuantityofSameProduct(int cartid,int productid,string userid);
        ReadCartVM ResetCart(int cartid);

    }
}

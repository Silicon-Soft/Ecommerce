using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface ICart_ItemsService
    {
        CreateCart_itemVM CreateCart_item(CreateCart_itemVM createCart_ItemVM);
    }
}

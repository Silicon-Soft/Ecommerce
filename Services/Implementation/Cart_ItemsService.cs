using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class Cart_ItemsService:ICart_ItemsService
    {
        private readonly IGenericReopsitory<Cart_item> _genericReopsitory;
        private IMapper _mapper;
        public Cart_ItemsService(IGenericReopsitory<Cart_item> genericReopsitory,IMapper mapper) {
        
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public CreateCart_itemVM CreateCart_item(CreateCart_itemVM createCart_ItemVM)
        {
            Cart_item cart_Item=_mapper.Map<Cart_item>(createCart_ItemVM);
            cart_Item = _genericReopsitory.Add(cart_Item);
            CreateCart_itemVM createCart_ItemVM1 = _mapper.Map<CreateCart_itemVM>(cart_Item);
            return createCart_ItemVM1;
        }
    }
}

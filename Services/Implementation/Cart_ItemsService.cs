using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Migrations;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.CodeAnalysis;

namespace Ecommerce.Services.Implementation
{
    public class Cart_ItemsService:ICart_ItemsService
    {
        private readonly IGenericReopsitory<Cart_item> _genericReopsitory;
        private readonly IGenericReopsitory<Cart> _genericReopsitory_cart;
        private readonly ICartService _cartService;
        private IMapper _mapper;
        public Cart_ItemsService(IGenericReopsitory<Cart_item> genericReopsitory,IMapper mapper,IGenericReopsitory<Cart> genericReopsitory_cart,ICartService cartService) 
        {
            _cartService = cartService;
            _genericReopsitory_cart = genericReopsitory_cart;
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public CreateCart_itemVM updateCart_item(CreateCart_itemVM createCart_ItemVM)
        {
            var cart_item = _genericReopsitory.GetById(createCart_ItemVM.Cart_itemId);

            var updatedCart_item = _mapper.Map<CreateCart_itemVM, Cart_item>(createCart_ItemVM, cart_item);
            cart_item = _genericReopsitory.Update(updatedCart_item);
            CreateCart_itemVM readCartVM1 = _mapper.Map<CreateCart_itemVM>(cart_item);
            return readCartVM1;
        }
        public CreateCart_itemVM CreateCart_item(CreateCart_itemVM createCart_ItemVM)
        {
            Cart_item cart_Item=_mapper.Map<Cart_item>(createCart_ItemVM);
            cart_Item = _genericReopsitory.Add(cart_Item);
            CreateCart_itemVM createCart_ItemVM1 = _mapper.Map<CreateCart_itemVM>(cart_Item);
            return createCart_ItemVM1;
        }
        public int getCartItemQuantity(string userid)
        {
            List<Cart> carts=_genericReopsitory_cart.GetAll();
            List<Cart_item> cart_Items=_genericReopsitory.GetAll();
            var query1=from c in carts where c.UserId == userid select c.CartId;
            int cartid = query1.FirstOrDefault();
            var query= from ci in cart_Items
                       join c in carts on ci.CartId equals c.CartId
                       where c.UserId == userid && ci.CartId == cartid
                       select ci;
            return query.Count();
        }
        public int getCartitemid(int cartid, string userid, int productid)
        {
            List<Cart> carts = _genericReopsitory_cart.GetAll();
            List<Cart_item> cart_Items = _genericReopsitory.GetAll();
            var query1 = from c in carts
                         join ci in cart_Items
                       on c.CartId equals ci.CartId
                         where c.CartId == cartid
             && c.UserId == userid && ci.ProductId == productid
                         select ci;
            if (query1.Count()<= 0)
            {
                return 0;
            }
            else
            {
                return query1.First().Cart_itemId;

            }
        }
        public List<ViewCart_itemVM> GetCart_item(string userid)
        {
            List<Cart> carts = _genericReopsitory_cart.GetAll();
            List<Cart_item> cart_Items = _genericReopsitory.GetAll();
            var query1 = from c in carts where c.UserId == userid select c.CartId;
            int cartid = query1.FirstOrDefault();
            var query = from ci in cart_Items
                        join c in carts on ci.CartId equals c.CartId
                        where c.UserId == userid && ci.CartId == cartid
                        select ci;
            List<Cart_item> cart_items=query.ToList();
            List<ViewCart_itemVM> viewCart_ItemVM = _mapper.Map<List<ViewCart_itemVM>>(cart_items);

            return viewCart_ItemVM;
        }
        public CreateCart_itemVM GetCreateCart_ItembyID(int id)
        {
            Cart_item cart_Item=_genericReopsitory.GetById(id);
            CreateCart_itemVM createCart_ItemVM=_mapper.Map<CreateCart_itemVM>(cart_Item);
            return createCart_ItemVM;
        }
        public void DeleteListofCart_item(List<ViewCart_itemVM> viewCart_ItemVMs)
        {
            try
            {
                foreach (var viewitem in viewCart_ItemVMs)
                {
                    DeleteCart_item(viewitem.Cart_itemId);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void DeleteCart_item(int id)
        {
            _genericReopsitory.Delete(id);

        }
    }
}

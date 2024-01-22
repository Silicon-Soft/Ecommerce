using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Ecommerce.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericReopsitory<Cart> _genericReopsitory;
        private readonly IGenericReopsitory<Cart_item> _genericReopsitory_cartitems;
        private readonly IMapper _mapper;
        public CartService(IGenericReopsitory<Cart> genericReopsitory, IMapper mapper, IGenericReopsitory<Cart_item> genericReopsitory_cartitems)
        {

            _mapper = mapper;
            _genericReopsitory = genericReopsitory;
            _genericReopsitory_cartitems = genericReopsitory_cartitems;
        }
        public ReadCartVM UpdateCart(CreatecartVM createcartVM)
        {

            var cart = _genericReopsitory.GetById(createcartVM.CartId);

            var updatedCart = _mapper.Map<CreatecartVM, Cart>(createcartVM, cart);
            cart = _genericReopsitory.Update(updatedCart);
            ReadCartVM readCartVM1 = _mapper.Map<ReadCartVM>(cart);
            return readCartVM1;
        }
        public bool IsproductIncart(int productId,string userid)
        {
            List <Cart> carts= _genericReopsitory.GetAll();
            List<Cart_item> cart_Items = _genericReopsitory_cartitems.GetAll();
            var quey=  from ci in cart_Items
                                  join c in carts on ci.CartId equals c.CartId
                                  where c.UserId == userid && ci.ProductId == productId
                                  select ci;
            var data=quey.ToList();
            int len=data.Count;
            if(len==0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool IsCartEmpty(string userId)
        {
            List<Cart> carts = _genericReopsitory.GetDatas().Where(x => x.UserId == userId).ToList();
            int lenght = carts.Count();

            if (lenght <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public CreatecartVM Createcart(CreatecartVM createcartVM)
        {
            Cart cart = _mapper.Map<Cart>(createcartVM);
            cart = _genericReopsitory.Add(cart);
            createcartVM = _mapper.Map<CreatecartVM>(cart);
            return createcartVM;

        }
        public ReadCartVM ReadCart(string userId)
        {
            Cart cart = _genericReopsitory.GetDatas().Where(x => x.UserId == userId).FirstOrDefault();
            ReadCartVM readCart = _mapper.Map<ReadCartVM>(cart);
            return readCart;
        }
        public int GetQuantityofSameProduct(int cartid, int productid,string userid)
        {
            List<Cart> carts = _genericReopsitory.GetAll();
            List<Cart_item> cart_Items=_genericReopsitory_cartitems.GetAll();
            var query = from ci in cart_Items
                        join
                      c in carts on ci.CartId equals c.CartId
                        where ci.ProductId == productid && c.UserId == userid && ci.CartId == cartid
                        select ci;
            return query.First().quantity;                   
        }
        public ReadCartVM ResetCart(int cartid)
        {
            Cart cart=_genericReopsitory.GetById(cartid);
            cart.total = 0;
            cart=_genericReopsitory.Update(cart);
            ReadCartVM readCartVM=_mapper.Map<ReadCartVM>(cart);
            return readCartVM;
        }
    }
}

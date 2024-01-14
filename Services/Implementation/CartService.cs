using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class CartService : ICartService
    {
        private readonly IGenericReopsitory<Cart> _genericReopsitory;
        private readonly IMapper _mapper;
        public CartService(IGenericReopsitory<Cart> genericReopsitory, IMapper mapper)
        {
            _mapper = mapper;
            _genericReopsitory = genericReopsitory;
        }
        public ReadCartVM UpdateCart(CreatecartVM createcartVM)
        {

            var cart = _genericReopsitory.GetById(createcartVM.CartId);

            var updatedCart = _mapper.Map<CreatecartVM, Cart>(createcartVM, cart);
            cart = _genericReopsitory.Update(updatedCart);
            ReadCartVM readCartVM1 = _mapper.Map<ReadCartVM>(cart);
            return readCartVM1;
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
    }
}

using AutoMapper;
using AutoMapper.Execution;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using System.Net;

namespace Ecommerce.Services.Implementation
{
    public class OrderService:IOrderService
    {
        private readonly IGenericReopsitory<Order> _genericReopsitory;
        private readonly IMapper _mapper;
        private readonly IShippingService _shippingService;
        private readonly ICartService _cartService;
        private readonly ICart_ItemsService _cart_ItemsService;
        private readonly IProductService _productService;
        public OrderService(IGenericReopsitory<Order> genericReopsitory,IMapper mapper,IShippingService shippingService,ICartService cartService,ICart_ItemsService cart_ItemsService,IProductService productService)
        {
            _mapper = mapper;
            _genericReopsitory = genericReopsitory;
            _productService = productService;
            _shippingService = shippingService;
            _cartService = cartService;
            _cart_ItemsService  = cart_ItemsService;
        }
        public CreateOrderVM CreateOrder(CreateOrderVM createOrderVM)
        {
            Order order=_mapper.Map<Order>(createOrderVM);
            order.dateTime= DateTime.Now;
            order = _genericReopsitory.Add(order);
            createOrderVM = _mapper.Map<CreateOrderVM>(order);
            return createOrderVM;
        }
        public ViewOrderVM GetOrderById(int id)
        {
            Order order = _genericReopsitory.GetById(id);
            ViewOrderVM viewOrderVM=_mapper.Map<ViewOrderVM>(order);
            return viewOrderVM;
        }
        public CreateOrderVM InitiateOrder(String userid, string customerNumber, string shipping)
        {
            int shippingid = int.Parse(shipping);
            ViewShippingCompanyVM viewShippingCompanyVM = _shippingService.Getshipping(shippingid);
            ReadCartVM readCartVM = _cartService.ReadCart(userid);
            CreateOrderVM createOrderVM = new()
            {
                UserId = userid,
                ShippingId = shippingid,
                status = "Not Delivered",
                address = viewShippingCompanyVM.ShippingAddress,
                customernumber = customerNumber,
                total = readCartVM.total

            };

            createOrderVM = CreateOrder(createOrderVM);
            return createOrderVM;
        }


    }
}

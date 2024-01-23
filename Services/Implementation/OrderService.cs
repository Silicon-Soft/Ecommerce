using AutoMapper;
using AutoMapper.Configuration.Annotations;
using AutoMapper.Execution;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<User> _userManager;
        public OrderService(UserManager<User> userManager,IGenericReopsitory<Order> genericReopsitory,IMapper mapper,IShippingService shippingService,ICartService cartService,ICart_ItemsService cart_ItemsService,IProductService productService)
        {
            _userManager = userManager;
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
        public List<ViewOrderVM> GetOrderByUserid(string userid)
        {
            IQueryable<Order> orders=_genericReopsitory.GetDatas().Where(u=>u.UserId==userid);
            List<Order> orders1 = orders.ToList();
            List<ViewOrderVM> viewOrderVMs = _mapper.Map<List<ViewOrderVM>>(orders1);
            return viewOrderVMs;

        }
        public ViewOrderVM UpdateStatus(ViewOrderVM viewOrderVM)
        {
          

            var order = _genericReopsitory.GetById(viewOrderVM.OrderId);
            var updatedorder=_mapper.Map<ViewOrderVM,Order>(viewOrderVM, order);
            order = _genericReopsitory.Update(updatedorder);
            viewOrderVM =_mapper.Map<ViewOrderVM>(order) ;
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
        public async Task<IEnumerable<ViewOrderVM>> GetAllOrder()
        {
            IQueryable<Order> orders = _genericReopsitory.GetDatas();
            IEnumerable<Order> allOrders = orders.ToList();
            IEnumerable<ViewOrderVM> viewOrderVMs = _mapper.Map<IEnumerable<ViewOrderVM>>(allOrders);

            foreach (var order in viewOrderVMs)
            {
                User user = await _userManager.FindByIdAsync(order.UserId);

                if (user != null)
                {
                    // Adjust property names based on your ApplicationUser model
                    order.Fullname = user.firstname + " " + user.lastname;
                }
                else
                {
                    // Handle the case where the user is not found, for example, set a default full name.
                    order.Fullname = "User Not Found";
                }
            }

            return viewOrderVMs;
        }



    }
}

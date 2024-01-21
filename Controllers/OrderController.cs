using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Ecommerce.Controllers
{
    
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly IShippingService _shippingService;
        private readonly ICartService _cartService;
        private readonly ICart_ItemsService _cart_ItemsService;
        private readonly IProductService _productService;
        
       public OrderController(IOrderService orderService,IOrderItemService orderItemService,IShippingService shippingService,ICartService cartService,ICart_ItemsService cart_ItemsService,IProductService productService) 
        {
            _productService = productService;
            _cart_ItemsService= cart_ItemsService;
            _cartService = cartService;
            _shippingService = shippingService;
            _orderItemService = orderItemService;
            _orderService = orderService;
        }
       
    public ActionResult provideaddress()
        {
            List<ViewShippingCompanyVM> viewShippingCompanyVMs = _shippingService.GetAllShippingCompany();
            var companies = viewShippingCompanyVMs.Select(x =>
            new
            {
                id=x.ShippingId,
                address=x.ShippingAddress
            });
            var jsonresult=JsonConvert.SerializeObject(companies);
            return Json(jsonresult);
        }
        
        public IActionResult PlaceOrder(string userid,string number,string address)
        {
            try
            {
                CreateOrderVM createOrderVM= _orderService.InitiateOrder(userid,number,address);
                List<ViewCart_itemVM> viewCart_ItemVMs=_cart_ItemsService.GetCart_item(userid);
                List<CreateOrderItemVM> createOrderItemVMs=_orderItemService.CreateListofOrders(viewCart_ItemVMs,createOrderVM);
                //delete the list of  cartitems
                _cart_ItemsService.DeleteListofCart_item(viewCart_ItemVMs);
                return RedirectToAction("Index","Home");
            }
            catch (Exception)
            {
                throw;
            }
        }
       

    }
}

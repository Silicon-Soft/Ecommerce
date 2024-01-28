using Ecommerce.Models;
using Ecommerce.Security;
using Ecommerce.Services.Implementation;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Printing;
using X.PagedList;

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
        private readonly UserManager<User> _userManager;

        
       public OrderController(UserManager<User> userManager,IOrderService orderService,IOrderItemService orderItemService,IShippingService shippingService,ICartService cartService,ICart_ItemsService cart_ItemsService,IProductService productService) 
        {
            
            _userManager = userManager;
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
        public async Task<IActionResult> UserOrder(int page = 1, int pagesize = 5)
        {
            string userid = _userManager.GetUserId(User);
            List<ViewOrderVM> viewOrderVMs=_orderService.GetOrderByUserid(userid);
            var model =await viewOrderVMs.ToPagedListAsync(page, pagesize);
            return View(model);
        }
        public IActionResult SearchOrders(int orderId)
        {
            ViewOrderVM viewOrderVM = _orderService.GetOrderById(orderId);
            if (viewOrderVM == null)
            {
                return Json(0); 
            }
            else
            {
                
                return Json(viewOrderVM.OrderId);
            }
        }

        public IActionResult UserOrderItem(int orderId) 
        {
            List<ViewOrderitemVM> viewOrderitemVMs=_orderItemService.GetOrderItemById(orderId);
            return View(viewOrderitemVMs);
        }
        public IActionResult UpdateStatus(int orderId,string status)
        {
            if (orderId != 0 && status != null)
            {
                ViewOrderVM viewOrderVM = _orderService.GetOrderById(orderId);
                viewOrderVM.status = status;
                viewOrderVM = _orderService.UpdateStatus(viewOrderVM);
                var data = new
                {
                    orderid = orderId,
                    orderstatus = status
                };
                return Json(data);
            }else
            {
                return Json(null);
            }

        }

        [HttpGet]
        public IActionResult PlaceOrder(string userid, string number, string address)
        {
            try
            {
                // Initiate order
                CreateOrderVM createOrderVM = _orderService.InitiateOrder(userid, number, address);

                // Get cart items
                List<ViewCart_itemVM> viewCart_ItemVMs = _cart_ItemsService.GetCart_item(userid);

                // Create order items
                List<CreateOrderItemVM> createOrderItemVMs = _orderItemService.CreateListofOrders(viewCart_ItemVMs, createOrderVM);

                // Delete the list of cart items
                _cart_ItemsService.DeleteListofCart_item(viewCart_ItemVMs);

                // Reset the cart
                ReadCartVM readCartVM = _cartService.ReadCart(userid);
                _cartService.ResetCart(readCartVM.CartId);

            
               
                return Json("hi");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                Console.Error.WriteLine($"Error in PlaceOrder: {ex.Message}");
                return RedirectToAction("Error", "Home"); // Redirect to an error page or handle the error
            }
        }
    }
}

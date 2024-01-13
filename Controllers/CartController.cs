using Ecommerce.Constraints;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace Ecommerce.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private  UserManager<User> _user;
        private readonly ICartService _cartService;
        private readonly IGenericReopsitory<Cart> _genericReopsitory;
        private readonly IProductService _productService;

        public CartController (UserManager<User> user,ICartService cartService,IGenericReopsitory<Cart> genericReopsitory,IProductService productService)
        {
            _productService = productService;
            _genericReopsitory = genericReopsitory;
            _cartService = cartService;
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
       

        [HttpPost]
        public IActionResult AddtoCart(int productId)
        {
            
            //get the productdetail
            ReadProductVM readProductVM = _productService.GetReadProductVM(productId);
            string userId = _user.GetUserId(User);
            // check if user has cart or not //user's cart is empty
            if (_cartService.IsCartEmpty(userId))
            {
                // if empty create the cart table 
                // create cartitem update the quantity 
                
            }

            return Json(new { success = true });
        }
    }
}

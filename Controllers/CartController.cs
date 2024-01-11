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
        private readonly IGenericReopsitory<Product> _genericReopsitory;
        public CartController (UserManager<User> user,ICartService cartService,IGenericReopsitory<Product> genericReopsitory)
        {
            _genericReopsitory = genericReopsitory;
            _cartService = cartService;
            _user = user;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddtoCart(int productId)
        {
            string userid = _user.GetUserId(User);
            Product product=_genericReopsitory.GetById(productId);
            if(_cartService.IsCartEmpty(userid))
            {
                CartVM cartVM = _cartService.createCart();
                cartVM.UserId = userid;
                cartVM.products.Append(product);
            }
            return View();
        }
    }
}

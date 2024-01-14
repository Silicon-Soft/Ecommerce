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
        private readonly ICart_ItemsService _cartItemsService;

        public CartController (UserManager<User> user,ICartService cartService,IGenericReopsitory<Cart> genericReopsitory,IProductService productService,ICart_ItemsService cart_ItemsService)
        {
            _cartItemsService = cart_ItemsService;
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
                CreatecartVM _createcartVM = new()
                {
                    UserId = userId,
                    total = 0,
                };
                // if empty create the cart table 
                CreatecartVM createcartVM = _cartService.Createcart(_createcartVM);
                //find the cartid using userid
                //ReadCartVM readCartVM = _cartService.ReadCart(userId);
                // create cartitem update the quantity 
                CreateCart_itemVM createCart_ItemVM = new()
                {
                    CartId = createcartVM.CartId,
                    ProductId = productId,
                    quantity = 1
                };
                CreateCart_itemVM createCart_ItemVM1 = _cartItemsService.CreateCart_item(createCart_ItemVM);

                // update the cart with the total 
                createcartVM.total = readProductVM.SellPrice;
                ReadCartVM readCart=_cartService.UpdateCart(createcartVM);
                return Json(readCart);

            }
            else
            {

                return Json(0);
            }

           
        }
    }
}

using Ecommerce.Constraints;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Schema;

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

        [HttpPost]
        public IActionResult Minuscart_item(int cart_itemid)
        {
            CreateCart_itemVM createCart_ItemVM=_cartItemsService.GetCreateCart_ItembyID(cart_itemid);
            createCart_ItemVM.quantity = createCart_ItemVM.quantity - 1;
            createCart_ItemVM = _cartItemsService.updateCart_item(createCart_ItemVM);
            ReadCartVM readCartVM = _cartService.ReadCart(_user.GetUserId(User));
            ReadProductVM readProductVM = _productService.GetReadProductVM(createCart_ItemVM.ProductId);
            CreatecartVM createcartVM = new()
            {
                CartId = createCart_ItemVM.CartId,
                UserId=_user.GetUserId(User),
                total=readCartVM.total-readProductVM.SellPrice
            };
            readCartVM = _cartService.UpdateCart(createcartVM);
            var jsondata = new
            {
                quantity=createCart_ItemVM.quantity,
                total=readCartVM.total
            };
            return Json(jsondata);
        }
        [HttpPost]
        public IActionResult pluscart_item(int cart_itemid)
        {
            CreateCart_itemVM createCart_ItemVM = _cartItemsService.GetCreateCart_ItembyID(cart_itemid);
            createCart_ItemVM.quantity = createCart_ItemVM.quantity + 1;
            createCart_ItemVM = _cartItemsService.updateCart_item(createCart_ItemVM);
            ReadCartVM readCartVM = _cartService.ReadCart(_user.GetUserId(User));
            ReadProductVM readProductVM = _productService.GetReadProductVM(createCart_ItemVM.ProductId);
            CreatecartVM createcartVM = new()
            {
                CartId = createCart_ItemVM.CartId,
                UserId = _user.GetUserId(User),
                total = readCartVM.total + readProductVM.SellPrice
            };
            readCartVM = _cartService.UpdateCart(createcartVM);
            var jsondata = new
            {
                quantity = createCart_ItemVM.quantity,
                total = readCartVM.total
            };
            return Json(jsondata);
        }

        [HttpPost]
        public IActionResult AddtoCart(ViewProductVM viewProductVM)
        {
            
            //get the productdetail
            ReadProductVM readProductVM = _productService.GetReadProductVM(viewProductVM.ProductID);
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
                    ProductId = viewProductVM.ProductID,
                    quantity = 1
                };
                CreateCart_itemVM createCart_ItemVM1 = _cartItemsService.CreateCart_item(createCart_ItemVM);

                // update the cart with the total 
                createcartVM.total = readProductVM.SellPrice;
                ReadCartVM readCart=_cartService.UpdateCart(createcartVM);
               
                return Json(viewProductVM);

            }
            else
            {
                //get cart detail
                ReadCartVM readCartVM = _cartService.ReadCart(userId);
                //check if the same product is in the cart or not.
                if((_cartService.IsproductIncart(readProductVM.ProductID,userId)))
                {
                    int tobeupadatequantity = _cartService.GetQuantityofSameProduct(readCartVM.CartId, readProductVM.ProductID, _user.GetUserId(User));
                    int cart_itemid = _cartItemsService.getCartitemid(readCartVM.CartId, _user.GetUserId(User), readProductVM.ProductID);
                    //upadate the cartitem table.
                    CreateCart_itemVM createCart_ItemVM = new()
                    {
                        CartId = readCartVM.CartId,
                        Cart_itemId = cart_itemid,
                        ProductId = readProductVM.ProductID,
                        quantity = tobeupadatequantity + 1
                    };
                    createCart_ItemVM = _cartItemsService.updateCart_item(createCart_ItemVM);
                    // update the cart
                    //get the sellprice of the product

                    readCartVM.total = readCartVM.total + viewProductVM.SellPrice;
                    CreatecartVM createcartVM = new()
                    {
                        CartId = readCartVM.CartId,
                        UserId = _user.GetUserId(User),
                        total = readCartVM.total
                    };
                    readCartVM = _cartService.UpdateCart(createcartVM);
                    return Json(viewProductVM);
                    //////
                    //if empty create new cart_item
                   

                }
               
                //if yes upadate the quantity.
                else
                {
                    CreateCart_itemVM createCartItemVM = new()
                    {
                        CartId = readCartVM.CartId,
                        ProductId = viewProductVM.ProductID,
                        quantity = 1
                    };
                    createCartItemVM = _cartItemsService.CreateCart_item(createCartItemVM);
                    readCartVM.total = readCartVM.total + viewProductVM.SellPrice;
                    CreatecartVM createcartVM = new()
                    {
                        CartId = readCartVM.CartId,
                        UserId = userId,
                        total = readCartVM.total,
                    };

                    readCartVM = _cartService.UpdateCart(createcartVM);
                    return Json(viewProductVM);
                }
            }
        }
        public IActionResult ViewCart(string userid)
        {
            
            List<ViewCart_itemVM> viewCart_=_cartItemsService.GetCart_item(userid);
            foreach(var cart_item in viewCart_)
            {
                cart_item.imgLink = _productService.GetProductByID(cart_item.ProductId).imageLink;
                cart_item.ProductPrice=_productService.GetPrice(cart_item.ProductId);
                cart_item.productname = _productService.GetProductByID(cart_item.ProductId).ProductName;
                cart_item.total = _cartService.ReadCart(userid).total;

            }
            return View(viewCart_);
        }
        [HttpPost]
        public IActionResult DeleteCart_item(int cart_itemid)
        {
            CreateCart_itemVM createCart_ItemVM=_cartItemsService.GetCreateCart_ItembyID(cart_itemid);
            decimal price = _productService.GetPrice(createCart_ItemVM.ProductId);
            decimal pricetobereduced = price * createCart_ItemVM.quantity;
            ReadCartVM readCartVM = _cartService.ReadCart(_user.GetUserId(User));
            CreatecartVM createcartVM = new()
            {
                CartId =createCart_ItemVM.CartId,
                UserId=_user.GetUserId(User),
                total=readCartVM.total-pricetobereduced
            };
            readCartVM= _cartService.UpdateCart(createcartVM);
            _cartItemsService.DeleteCart_item(cart_itemid);

            var jsondata = new
            {
                cartitem_id = createCart_ItemVM.Cart_itemId,
                total = readCartVM.total
            };

            return Json(jsondata);
        }
        public IActionResult BuyCart()
        {
            ReadCartVM readCartVM = _cartService.ReadCart(_user.GetUserId(User));
            List<ViewCart_itemVM> cart_ItemVMs = _cartItemsService.GetCart_item(_user.GetUserId(User));
            foreach(var obj in cart_ItemVMs)
            {
                obj.total = readCartVM.total;
                obj.productname = _productService.GetReadProductVM(obj.ProductId).ProductName;
                obj.ProductPrice = _productService.GetPrice(obj.ProductId);
            }

            return View(cart_ItemVMs);
        }
    }
}

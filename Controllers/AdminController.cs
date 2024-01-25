using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PagedList.Mvc;
using PagedList;
using X.PagedList;
using X.PagedList.Mvc.Core;
using System.Drawing.Printing;

namespace Ecommerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IShippingService _shippingService;
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;
        private readonly UserManager<User> _userManager;
        public AdminController(UserManager<User> userManager,IOrderItemService orderItemService,ICategoryService categoryService,IProductService productService, IWebHostEnvironment hostingEnvironment, IShippingService shippingService, IOrderService orderService)
        {
            _userManager = userManager;
            _categoryService = categoryService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _shippingService = shippingService;
            _orderService = orderService;
            _orderItemService = orderItemService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(CategoryVM categoryVM)
        {
            _categoryService.AddCategory(categoryVM);
            return View("Index");

        }

        public async Task<IActionResult> ViewOrders(int page=1,int pagesize=5)
        {
            IEnumerable<ViewOrderVM> viewOrderVMs = await _orderService.GetAllOrder();
            var model = await viewOrderVMs.ToPagedListAsync(page, pagesize);
            return View(model);
        }
        


        public IActionResult AddCompany()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCompany(CreateShippingVM createShippingVM)
        {
            if (ModelState.IsValid)
            {
                createShippingVM = _shippingService.CreateShipping(createShippingVM);
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult ViewShippingCompany()
        {
            List<ViewShippingCompanyVM> viewShippingCompanyVMs = _shippingService.GetAllShippingCompany();
            return View(viewShippingCompanyVMs);
        }
        public IActionResult DeleteCompany(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            _shippingService.Deleteshipping(id);
            TempData["Delete"] = "Record Deleted Successfully !";
            return RedirectToAction("ViewShippingCompany", "Admin");
        }
        public IActionResult ViewOrderDetail(int orderid)
        {
            List<ViewOrderitemVM> viewOrderitemVMs=_orderItemService.GetOrderItemById(orderid);
            return View(viewOrderitemVMs);
        }


    }
}

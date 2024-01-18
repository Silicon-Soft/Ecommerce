using Ecommerce.Data;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IShippingService _shippingService;
        public AdminController(ICategoryService categoryService,IProductService productService, IWebHostEnvironment hostingEnvironment, IShippingService shippingService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
            _shippingService = shippingService;
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
        
        public IActionResult ViewOrders()
        {
            return View();
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
       
    }
}

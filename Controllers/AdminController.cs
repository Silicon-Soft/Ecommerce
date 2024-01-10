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
        public AdminController(ICategoryService categoryService,IProductService productService, IWebHostEnvironment hostingEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _hostingEnvironment = hostingEnvironment;
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

    }
}

using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Ecommerce.Controllers
{

    public class HomeController : Controller
    {
        
        private readonly IProductService _productService;
        private readonly IGenericReopsitory<Product> _genericReopsitoryProduct;
        public HomeController(IProductService productService,IGenericReopsitory<Product> genericReopsitoryProduct)
        {
            _genericReopsitoryProduct= genericReopsitoryProduct;
            _productService = productService;
            
        }

        public IActionResult Index(string searching="")
        {
            List<ViewProductVM> products = _productService.GetProducts();

            if (string.IsNullOrEmpty(searching))
            {
                return View(products);
            }
            else
            {
                searching = searching.ToLower();
                foreach (var product in products)
                {
                    product.LowerProductName = product.ProductName;
                    string s = product.LowerProductName.ToLower();
                    product.LowerProductName = s;
                }
                List<ViewProductVM> viewproducts = products.Where(x => x.LowerProductName.Contains(searching) || searching == null).ToList()!;
                return View(viewproducts);
            }
        }

    public IActionResult ProductDetail(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            ReadProductVM readProductVM=_productService.GetReadProductVM(id);
            if (readProductVM == null)
            {
                return NotFound();
            }
            return View(readProductVM);


        }
    [HttpPost]
    public IActionResult ProductDetail(ReadProductVM readProductVM)
        {
            return View(readProductVM);
        }
    


    public IActionResult Privacy()
         
        {
            return View();
        }
    public IActionResult UserOrder()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
   
    }
}

using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecommerce.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public ProductController(ICategoryService categoryService, IProductService productService, IWebHostEnvironment hostEnvironment)
        {
            _categoryService = categoryService;
            _productService = productService;
            _hostingEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddProduct()
        {
            List<ViewCategoryVM> viewCategoryVMs = _categoryService.GetCategories(); //good
            var model = new CreateProductVM
            {
                CategoriesSelectList = new List<SelectListItem>()
            };

            foreach (var category in viewCategoryVMs)
            {
                model.CategoriesSelectList.Add(new SelectListItem { Text = category.CategoryName, Value = category.CategoryId.ToString() });
            }

            return View(model);
        }
        [HttpPost]
        public IActionResult AddProduct(CreateProductVM createProductVM)
        {
            var selectedCategory = createProductVM.SelectedCategory;
            int categoryid = int.Parse(selectedCategory);

            if (createProductVM.imagefile != null)
            {
                createProductVM.imagelink = _productService.CreateImage(createProductVM.imagefile);
                createProductVM.CategoryId = categoryid;
            }

            CreateProductVM obj= _productService.Addproduct(createProductVM);
            TempData["ResultOk"] = "Record Added Successfully !";

            return RedirectToAction("Index", "Admin");
        }
        public IActionResult ViewProduct()
        {
            List<ViewProductVM> viewProductVMs = _productService.GetProducts();
            return View(viewProductVMs);

        }
        public IActionResult EditProduct(int id)
        
        {
            if (id <= 0)
            {
                return NotFound();
            }

            EditProductVM editProductVM = _productService.GetProductByID(id);

            if (editProductVM == null)
            {
                return NotFound();
            }
            return View(editProductVM);
        }
        [HttpPost]
        public IActionResult EditProduct(EditProductVM editProductVM)
       {
            if (editProductVM.imageLink != editProductVM.oldimageLink)
            {
                if (editProductVM.imagefile != null)
                {

                    editProductVM.imageLink = _productService.CreateImage(editProductVM.imagefile);
                    _productService.DeleteImage(editProductVM.oldimageLink);
                }
            }
            else
            {
                editProductVM.imageLink = editProductVM.oldimageLink;
            }
            try
            {
                
                _productService.EditProduct(editProductVM);
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Admin");
            ;
        }
        public IActionResult DeleteProduct(int id)
        {
            if(id == 0 )
            {
                return NotFound() ;
            }
            string imagefile = _productService.GetLinkById(id);
            int deletedID=_productService.DeleteProduct(id);
            string deleted_image=_productService.DeleteImage(imagefile);
            TempData["Delete"] = "Record Deleted Successfully !";
            return RedirectToAction("ViewProduct","Product");
        }
        public IActionResult ReadProduct(int id)
        {
            ReadProductVM readProductVM= _productService.GetReadProductVM(id);

            return View(readProductVM);
        }

    }
}

using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class ProductService:IProductService
    {
        private readonly IGenericReopsitory<Product> _genericReopsitory;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private IMapper _mapper;
        public ProductService(IGenericReopsitory<Product> genericReopsitory,IMapper mapper,ICategoryService categoryService,IWebHostEnvironment hostingEnvironment) 
        {
            _hostingEnvironment = hostingEnvironment;
            _categoryService = categoryService;
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public CreateProductVM Addproduct(CreateProductVM createProductVM)
        {
            try
            {
                Product product = _mapper.Map<Product>(createProductVM);
                _genericReopsitory.Add(product);
                return createProductVM;
            }
            catch (Exception)
            {
                throw;
            }
            

        }
        public List<ViewProductVM> GetProducts()
        {
            try
            {
                List<Product> products = _genericReopsitory.GetAll();
                List<ViewProductVM> viewProductVMs = _mapper.Map<List<ViewProductVM>>(products);
                foreach (var product in viewProductVMs)
                {
                    CategoryVM categoryVM = _categoryService.GetCategory(product.CategoryId);
                    product.CategoryName = categoryVM.CategoryName;

                }
                return viewProductVMs;
            }
            catch(Exception)
            {
                throw;
            }
        }
        public EditProductVM GetProductByID(int id)
        {
            try
            {
                Product product = _genericReopsitory.GetById(id);
                EditProductVM editProductVM = _mapper.Map<EditProductVM>(product);
                editProductVM.oldimageLink = product.imageLink;
                return editProductVM;

            }
            catch(Exception)
            {
                throw;
            }

            
        }
        public EditProductVM EditProduct(EditProductVM editProductVM)
        {
            try
            {
                Product product = _mapper.Map<Product>(editProductVM);
                _genericReopsitory.Update(product);
                EditProductVM editProduct = _mapper.Map<EditProductVM>(product);
                return editProduct;
            }
            catch(Exception) 
            {
                throw;
            }
            
            }
        public int DeleteProduct(int id)
        {
            try
            {
                _genericReopsitory.Delete(id);
                return id;
            }
            catch(Exception)
            {
                throw;
            }


        }
        public string DeleteImage(string link)
        {
            try
            {
                String uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "Product");
                String filename = link;
                String filepath = Path.Combine(uploadfolder, filename);
                File.Delete(filepath);
                return filepath;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public string CreateImage(IFormFile imagefile)
        {
            try
            {
                String uploadfolder = Path.Combine(_hostingEnvironment.WebRootPath, "Product");
                string filename = Guid.NewGuid().ToString() + "_" + imagefile.FileName;
                String filepath = Path.Combine(uploadfolder, filename);
                imagefile.CopyTo(new FileStream(filepath, FileMode.Create));
                return filename;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public ReadProductVM GetReadProductVM(int id)
        {
            try
            {
                Product product = _genericReopsitory.GetById(id);
                ReadProductVM readProductVM = _mapper.Map<ReadProductVM>(product);
                CategoryVM categoryVM=_categoryService.GetCategory(readProductVM.CategoryId);
                readProductVM.CategoryName=categoryVM.CategoryName;
                return readProductVM;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string GetLinkById(int id)
        {
            try
            {
                Product product = _genericReopsitory.GetById(id);
                return product.imageLink;

            }
            catch (Exception) 
            {
                throw;
            }
            }
    }
}

using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface IProductService
    {
        CreateProductVM Addproduct(CreateProductVM createProductVM);
        List<ViewProductVM> GetProducts();
        EditProductVM GetProductByID(int id);
        EditProductVM EditProduct(EditProductVM editProductVM);
        int DeleteProduct(int id);
        string DeleteImage(string imagefile);
        string CreateImage(IFormFile link);
        ReadProductVM GetReadProductVM(int id);
        string GetLinkById(int id);
        
        decimal GetPrice(int Productid);
        List<ViewProductVM> GetProductBycategory(int categoryid);
    }
}

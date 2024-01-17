using AutoMapper;
using Ecommerce.Models;
using Ecommerce.ViewModel;

namespace Ecommerce.MapperProfile
{
    public class MapperProfile:Profile
    {
        public MapperProfile() {
            CreateMap<Category, CategoryVM>();
            CreateMap<CategoryVM, Category>();
            CreateMap<Category,ViewCategoryVM>().ReverseMap();
            CreateMap<CreateProductVM, Product>();
            CreateMap<Product, ViewProductVM>();
            CreateMap<Product,EditProductVM>().ReverseMap();
            CreateMap<Product, ReadProductVM>();
            CreateMap<CreatecartVM, Cart>().ReverseMap();
            CreateMap<Cart,ReadCartVM>().ReverseMap();
            CreateMap<Cart_item, CreateCart_itemVM>().ReverseMap();
            CreateMap<Cart_item, ViewCart_itemVM>().ReverseMap();
            CreateMap<CreateCart_itemVM, ViewCart_itemVM>().ReverseMap();
            CreateMap<CreateShippingVM, ShippingCompany>().ReverseMap();

        }
    }
}

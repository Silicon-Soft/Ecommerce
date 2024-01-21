using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class ShippingService : IShippingService
    {
        private readonly IGenericReopsitory<ShippingCompany> _genericReopsitory;
        private IMapper _mapper;
        public ShippingService(IGenericReopsitory<ShippingCompany> genericReopsitory,IMapper mapper)
        { 
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public CreateShippingVM CreateShipping(CreateShippingVM createShippingVM)
        {
            try
            {
                ShippingCompany shippingCompany = _mapper.Map<ShippingCompany>(createShippingVM);   
                shippingCompany = _genericReopsitory.Add(shippingCompany);
                createShippingVM = _mapper.Map<CreateShippingVM>(shippingCompany);
                return createShippingVM;
            }
            catch (Exception)
            {
                throw;
            }

            

        }
        public List<ViewShippingCompanyVM> GetAllShippingCompany()
        {
            List<ShippingCompany> shippingCompanies=_genericReopsitory.GetAll().ToList();
            List<ViewShippingCompanyVM> viewShippingCompanyVMs = _mapper.Map<List<ViewShippingCompanyVM>>(shippingCompanies);
            return viewShippingCompanyVMs;
        }
        public void Deleteshipping(int id)
        {
            _genericReopsitory.Delete(id);
        }
        public ViewShippingCompanyVM Getshipping(int id)
        {
            ShippingCompany shippingCompany=_genericReopsitory.GetById(id);
            ViewShippingCompanyVM viewShippingCompanyVM=_mapper.Map<ViewShippingCompanyVM>(shippingCompany);    
            return viewShippingCompanyVM;
        }
    }
}

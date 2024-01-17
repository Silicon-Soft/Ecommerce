using AutoMapper;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Models;
using Ecommerce.Services.Interface;
using Ecommerce.ViewModel;

namespace Ecommerce.Services.Implementation
{
    public class ShippingService:IShippingService
    {
        private readonly IGenericReopsitory<ShippingService> _genericReopsitory;
        private IMapper _mapper;
        public ShippingService(IGenericReopsitory<ShippingService> genericReopsitory,IMapper mapper)
        { 
            _genericReopsitory = genericReopsitory;
            _mapper = mapper;
        }
        public CreateShippingVM CreateShipping(CreateShippingVM createShippingVM)
        {
            ShippingCompany company = _mapper.Map<ShippingCompany>(createShippingVM);
        }
    }
}

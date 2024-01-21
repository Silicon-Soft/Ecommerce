﻿using Ecommerce.ViewModel;

namespace Ecommerce.Services.Interface
{
    public interface IShippingService
    {
        CreateShippingVM CreateShipping(CreateShippingVM createShippingVM);
        List<ViewShippingCompanyVM> GetAllShippingCompany();
        void Deleteshipping(int id);
        ViewShippingCompanyVM Getshipping(int id);
    }
}

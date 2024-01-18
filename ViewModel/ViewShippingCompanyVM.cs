using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class ViewShippingCompanyVM
    {
        public int ShippingId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        [MaxLength(10)]
        public string phonenumber { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Models
{
    public class ShippingCompany
    {
        [Key]
        public int ShippingId { get; set; }
        public string ShippingName { get; set; }
        public string ShippingAddress { get; set; }
        [MaxLength(10)]
        public string phonenumber { get; set; }
    }
}

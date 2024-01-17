using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.ViewModel
{
    public class CreateShippingVM
    {
        [Required]
        [DisplayName("ShippingName")]
        public string ShippingName { get; set; }
        [Required(ErrorMessage = "*")]
        public string ShippingAddress { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(10)]
        public string phonenumber { get; set; }
    }
}

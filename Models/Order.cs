using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


        public string status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
        public int ShippingId { get; set; }
        [ForeignKey("ShippingId")]
        public virtual ShippingCompany ShippingCompany { get; set; }

        public string address { get; set; }
    }
}

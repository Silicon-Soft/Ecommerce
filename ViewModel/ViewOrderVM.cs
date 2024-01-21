using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class ViewOrderVM
    {
        [Key]
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public string status { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
        public int ShippingId { get; set; }
    

        public string address { get; set; }
        public string customernumber { get; set; }
        public DateTime dateTime { get; set; }
    }
}

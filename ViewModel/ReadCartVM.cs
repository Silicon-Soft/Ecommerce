using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class ReadCartVM
    {
        public int CartId { get; set; }
        public string UserId { get; set; }
     

        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
    }
}

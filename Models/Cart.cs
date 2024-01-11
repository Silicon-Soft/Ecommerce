using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Cart
    {
        
        public int CartId { get; set;}
        public string UserId { get; set;}
        [ForeignKey("UserId")]
        public virtual User User { get; set;}

        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Cart_item
    {
        public int quantity { get; set; }
        public virtual Cart cart { get; set; }
        [ForeignKey(nameof(cart))]
        public int CartId { get; set; }
        public virtual Product Product { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
    }
}

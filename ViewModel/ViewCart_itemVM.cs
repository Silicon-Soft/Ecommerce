using Ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class ViewCart_itemVM
    {
        public int Cart_itemId { get; set; }
        public int CartId { get; set; }
        

        public int ProductId { get; set; }
        public decimal ProductPrice { get; set; }
        

        public int quantity { get; set; }
        public string imgLink { get; set; }
        public string productname { get; set; }
    }
}

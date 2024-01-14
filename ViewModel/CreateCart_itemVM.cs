using Ecommerce.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class CreateCart_itemVM
    {
        public int CartId { get; set; }
      

        public int ProductId { get; set; }
  

        public int quantity { get; set; }
    }
}

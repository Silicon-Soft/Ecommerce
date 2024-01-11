using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class ViewProductVM
    {
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string LowerProductName { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }
        public string imageLink { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


    }
}

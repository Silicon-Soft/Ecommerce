using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class EditProductVM
    {
        [Key]
        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SellPrice { get; set; }

        public int Quantity { get; set; }
        public string oldimageLink { get; set; }
        public IFormFile oldimagefile { get; set; }
        public string imageLink { get; set; }
        [Required]
        public IFormFile imagefile { get; set; }
    }
}

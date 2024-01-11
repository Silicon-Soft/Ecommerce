﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    public class Cart_item
    {
        public int CartId { get; set; }
        [ForeignKey("CartId")]
        public virtual Cart Cart { get; set; }  

        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}

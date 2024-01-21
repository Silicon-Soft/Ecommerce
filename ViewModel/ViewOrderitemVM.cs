﻿using Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.ViewModel
{
    public class ViewOrderitemVM
    {
        public int orderitem_id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        [Required]
        public string productname { get; set; }

        [Required]
        public int quantity { get; set; }
        [Required]
        public decimal basictotal { get; set; }
    }
}
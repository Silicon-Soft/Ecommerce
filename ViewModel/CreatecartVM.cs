using Ecommerce.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;



    public class CreatecartVM
    {
        public string UserId { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal total { get; set; }
    }


using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Models
{
    public class User:IdentityUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
       
    }
}

using Ecommerce.Constraints;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace Ecommerce.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesAndAdminAsync(IServiceProvider service)
        {
            var userManager=service.GetService<UserManager<User>>();
            var roleManager=service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            var user = new User
            {
                UserName = "admin12@gmail.com",
                Email = "admin12@gmail.com",
                firstname="admin",
                lastname="",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            var userInDb= await userManager.FindByEmailAsync(user.Email);
            if (userInDb == null)
            {
                await userManager.CreateAsync(user,"Admin@123");
                await userManager.AddToRoleAsync(user,Roles.Admin.ToString());
            }
        }
    }
}

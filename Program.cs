using Ecommerce.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Ecommerce.Models;
using Ecommerce.GenericRepository.Implementation;
using Ecommerce.GenericRepository.Interface;
using Ecommerce.Services.Interface;
using Ecommerce.Services.Implementation;

namespace Ecommerce
{
    public class Program
    {
        public static async Task  Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<EcommerceDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddIdentity<User, IdentityRole>()
                 .AddEntityFrameworkStores<EcommerceDbContext>()
                 .AddDefaultUI()
                 .AddDefaultTokenProviders();
            builder.Services.AddScoped<ICategoryService,CategoryService>();
            builder.Services.AddScoped<IProductService,ProductService>();
            builder.Services.AddScoped<ICartService,CartService>();
            builder.Services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            builder.Services.AddScoped(typeof(IGenericReopsitory<>), typeof(GenericRepository<>));
            builder.Services.AddControllersWithViews();
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddDistributedMemoryCache();
            



            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapRazorPages();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            using (var scope = app.Services.CreateScope())
            {
                await DbSeeder.SeedRolesAndAdminAsync(scope.ServiceProvider);
            }
            app.Run();
        }
    }
}
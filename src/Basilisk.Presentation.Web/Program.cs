using Basilisk.Presentation.Web.Configuration;
using Basilisk.Presentation.Web.Services;
using Basilisk.DataAccess;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Basilisk.Presentation.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConsole(); //Instansiasi dari Ilogger

            //dependency injection
            //Mmebuat model view dan Controller
            IServiceCollection services = builder.Services;
            Dependencies.AddDataAccessServices(services, builder.Configuration);

            services.AddBusinessServices();
            
            services.AddScoped<CategoryService>();
            services.AddScoped<ProductService>();
            services.AddScoped<SupplierService>();
            services.AddScoped<AuthService>();
            services.AddControllersWithViews();

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "AuthenticationTicket";
                    options.LoginPath = "/Login";
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/AccessDenied";
                }); //Konfigurasi Cookies

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); //Menggunakan Authentication
            app.UseAuthorization();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=HomePage}"
                );

            app.Run();
        }
    }
}
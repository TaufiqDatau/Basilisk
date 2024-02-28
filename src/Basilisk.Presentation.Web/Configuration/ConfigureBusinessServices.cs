using System.Runtime.CompilerServices;
using Basilisk.Busines.Repositories;
using Basilisk.Busines.Interface;
namespace Basilisk.Presentation.Web.Configuration
{
    public static class ConfigureBusinessServices
    {
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped< ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            return services;
        }
    }
}

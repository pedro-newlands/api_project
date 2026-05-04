using Microsoft.Extensions.DependencyInjection;
using PokeShop.Application.Services;

namespace PokeShop.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<ICenterService, CenterService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IStorageService, StorageService>();

            return services;
        }
    }
}
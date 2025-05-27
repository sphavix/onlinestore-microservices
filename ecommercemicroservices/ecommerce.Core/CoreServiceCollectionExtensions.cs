using ecommerce.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Core
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register domain services here
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
    }
}

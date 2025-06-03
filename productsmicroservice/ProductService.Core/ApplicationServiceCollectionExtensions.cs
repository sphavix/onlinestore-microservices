using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register your application services here
        // Example: services.AddScoped<IProductService, ProductService>();
        return services;
    }
}

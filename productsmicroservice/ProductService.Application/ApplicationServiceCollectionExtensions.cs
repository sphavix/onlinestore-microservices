using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Mappers;
using ProductService.Application.Services;

namespace ProductService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register your application services here
        services.AddScoped<IProductsService, ProductsService>();

        services.AddAutoMapper(typeof(ProductsMappingProfile).Assembly);
        return services;
    }
}

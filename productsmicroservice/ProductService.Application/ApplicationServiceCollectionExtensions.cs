using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Application.Mappers;
using ProductService.Application.Services;
using ProductService.Application.Validators;

namespace ProductService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register your application services here
        services.AddScoped<IProductsService, ProductsService>();

        services.AddValidatorsFromAssemblyContaining<CreateProductRequestValidator>();

        services.AddAutoMapper(typeof(ProductsMappingProfile).Assembly);
        return services;
    }
}

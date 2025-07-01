using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrdersService.Application.Mappers;
using OrdersService.Application.Services;
using OrdersService.Application.Validators;

namespace OrdersService.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register application services here

        services.AddValidatorsFromAssemblyContaining<CreateOrderRequestValidator>();

        services.AddAutoMapper(typeof(OrderMappingProfile).Assembly);

        services.AddScoped<IOrderService, OrderService>();

        return services;
    }
}

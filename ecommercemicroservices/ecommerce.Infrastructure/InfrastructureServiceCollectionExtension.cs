using ecommerce.Core.Abstractions;
using ecommerce.Infrastructure.Persistence;
using ecommerce.Infrastructure.Respositories;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Infrastructure;

public static class InfrastructureServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<DbContextConfigurations>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}

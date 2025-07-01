using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using OrdersService.Infrastructure.Repositories;

namespace OrdersService.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services here

        string connectionString = configuration.GetConnectionString("OrdersConnection")!;

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        services.AddScoped<IMongoDatabase>(provider =>
        {
            var client = provider.GetRequiredService<IMongoClient>();
            return client.GetDatabase("OrdersDb");
        });

        services.AddScoped<IOrdersRepository, OrdersRepository>();

        return services;
    }
}

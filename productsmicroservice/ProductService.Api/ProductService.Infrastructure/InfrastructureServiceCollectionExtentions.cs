using Microsoft.Extensions.DependencyInjection;

namespace ProductService.Infrastructure;

public static class InfrastructureServiceCollectionExtentions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {
        // Register your infrastructure services here
        // Example: services.AddSingleton<IMyService, MyService>();
        return services;
    }
}

using ecommerce.Core.Mapping;
using ecommerce.Core.Services;
using ecommerce.Core.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ecommerce.Core
{
    public static class CoreServiceCollectionExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            // Register domain services here
            services.AddScoped<IUsersService, UsersService>();
            services.AddAutoMapper(typeof(MappingProfile).Assembly);

            services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();
            return services;
        }
    }
}

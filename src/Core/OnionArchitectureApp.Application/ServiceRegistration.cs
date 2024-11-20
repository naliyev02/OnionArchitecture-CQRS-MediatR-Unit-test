using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureApp.Application.Features.Queries.Products;
using System.Reflection;

namespace OnionArchitectureApp.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductQueryHandler).Assembly));
        return services;
    }
}

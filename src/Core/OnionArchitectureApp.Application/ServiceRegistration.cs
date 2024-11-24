using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OnionArchitectureApp.Application.Features.Queries.Products;
using OnionArchitectureApp.Application.Validations.ProductValidators;
using System.Reflection;

namespace OnionArchitectureApp.Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetAllProductQueryHandler).Assembly));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<CreateProductValidator>(); 

        return services;
    }
}

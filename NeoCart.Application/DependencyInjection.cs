using Microsoft.Extensions.DependencyInjection;

namespace NeoCart.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(opts => opts.RegisterServicesFromAssemblyContaining(typeof(DependencyInjection)));
        return services;
    }
}
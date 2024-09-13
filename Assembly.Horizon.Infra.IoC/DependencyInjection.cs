using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Assembly.Horizon.Infra.Data;
using Assembly.Horizon.Application;
using Assembly.Horizon.Security;

namespace Assembly.Horizon.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddIocServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataServices(configuration);
        services.AddApplicationServices(configuration);

        services.AddJwtAuthentication(configuration);

        return services;
    }

}
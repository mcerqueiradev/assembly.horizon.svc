using Assembly.Horizon.Security.Interface;
using Assembly.Horizon.Security.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assembly.Horizon.Security;

public static class DependencyInjection
{
    //JWT
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDataProtectionService, DataProtectionService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IUserResolverService, UserResolverService>();
        services.AddHttpContextAccessor();

        return services;
    }
}
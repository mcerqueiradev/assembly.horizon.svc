using StackExchange.Redis;

namespace Assembly.Horizon.Infra.Data.Infrastructure;

public class RedisConfiguration
{
    public static IConnectionMultiplexer CreateConnection(string connectionString)
    {
        var options = ConfigurationOptions.Parse(connectionString);
        options.AbortOnConnectFail = false;
        return ConnectionMultiplexer.Connect(options);
    }
}
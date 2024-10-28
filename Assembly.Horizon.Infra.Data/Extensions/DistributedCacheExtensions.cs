using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Assembly.Horizon.Infra.Data.Extensions;

public static class DistributedCacheExtensions
{
    public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
    {
        var data = await cache.GetAsync(key);
        if (data == null) return default;

        return JsonSerializer.Deserialize<T>(data);
    }

    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan expiration)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiration
        };

        var data = JsonSerializer.SerializeToUtf8Bytes(value);
        await cache.SetAsync(key, data, options);
    }
}

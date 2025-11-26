using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;

namespace Hcb.Rnd.Pwn.Infrastructure.DI;

/// <summary>
/// https://www.milanjovanovic.tech/blog/hybrid-cache-in-aspnetcore-new-caching-library
/// </summary>
public static class HybridCacheDependencyInjection
{
    public static IServiceCollection AddHybridCache(this IServiceCollection services)
    {
        services.AddHybridCache(options =>
        {
            // Maximum size of cached items
            options.MaximumPayloadBytes = 1024 * 1024 * 10; // 10MB
            options.MaximumKeyLength = 512;

            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(120),
                LocalCacheExpiration = TimeSpan.FromMinutes(120)
            };
        });

        return services;
    }
}

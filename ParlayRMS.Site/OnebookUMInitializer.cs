using ONEbook.UM.AspNetCore.Filters;
using ONEbook.UM.Configurations;
using ONEbook.UM.Helpers;
using ONEbook.UM.Services;
using ONEbook.UM.Transport;

namespace ParlayRMS.Site;

using Microsoft.Extensions.Caching.Memory;
using System;
using ONEbook.UM.AspNetCore;

public static class OnebookUMInitializer
{
    public static IServiceCollection AddOnebookUM(this IServiceCollection services, IConfiguration configuration)
    {
        var umConfig = new UMClientConfiguration();
        services.AddSingleton(umConfig.Initialize(configuration));
        services.AddSingleton<CheckUMFilterAttribute>();
        services.AddSingleton<IUMCacheService, MemoryUmCacheService>();
        services.AddSingleton<IUMRestClient, UMRestClient>();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<IHttpContextHelper, HttpContextHelper>();
        services.AddSingleton<IMaintenanceService, MaintenanceService>();

        return services;
    }
}

public class MemoryUmCacheService(IMemoryCache cache) : IUMCacheService
{
    public T Get<T>(string key) => cache.Get<T>(key);

    public void Set<T>(string key, T value, TimeSpan absoluteExpiration)
        => cache.Set(key, value, new MemoryCacheEntryOptions().SetAbsoluteExpiration(absoluteExpiration));

    public void Remove(string key) => cache.Remove(key);
}
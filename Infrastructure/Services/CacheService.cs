﻿
using Domain.Repositories.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Shared.Helpers;

namespace Infrastructure.Services;


public class CacheService : ICacheService
{
    private readonly IMemoryCache _cache;

    public CacheService(IMemoryCache cache)
    {
        _cache = cache;
    }
    public void Set<T>(string key, T value) => _cache.Set(key, JsonConvert.SerializeObject(value), CacheHelper.CacheOptions());

    public T Get<T>(string key)
    {
        var value = _cache.Get<string>(key);

        if (!string.IsNullOrEmpty(value))
            return JsonConvert.DeserializeObject<T>(value);
        return default;
    }

    public bool Remove(string key)
    {
        var value = _cache.Get<string>(key);
        if (string.IsNullOrEmpty(value))
            return false;

        _cache.Remove(key);
        return true;
    }
}

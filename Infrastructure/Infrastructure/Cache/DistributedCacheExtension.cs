using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hawk.Infrastructure.Cache
{
    public static class DistributedCacheExtension
    {
        public static async Task<T> GetAsync<T>(this IDistributedCache cache, string key)
        {
            var result = await cache.GetStringAsync(key);

            if (result != null)
            {
                return JsonConvert.DeserializeObject<T>(result);
            }

            return default;
        }

        public static async Task SetAsync(this IDistributedCache cache, string key, object toCache,
            TimeSpan expirationTime)
        {
            var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(expirationTime);
            await cache.SetStringAsync(key, JsonConvert.SerializeObject(toCache, Formatting.Indented), cacheOptions);
        }

        public static async Task<T> GetOrCreateAsync<T>(this IDistributedCache cache, string key,
            TimeSpan expirationTime,
            Func<Task<T>> resultFactory)
        {
            var result = await GetAsync<T>(cache, key);
            if (result != null)
            {
                return result;
            }

            var currentResult = await resultFactory();
            SetAsync(cache, key, currentResult, expirationTime);
            return currentResult;
        }
    }
}
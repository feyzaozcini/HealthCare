using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Pusula.Training.HealthCare.Core.Helpers.Abstracts;
using Pusula.Training.HealthCare.Shared;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Pusula.Training.HealthCare.Core.Helpers.Concretes;

public class CacheHelper<TDto, TInput>(IDistributedCache distributedCache, ILogger logger) : ICacheHelper<TDto, TInput>
{
    //Nil Birlik ve Feyza Özçini Pair Programming ile Yapıldı.

    public async Task<PagedResultDto<TDto>> GetOrAddAsync(Func<Task<PagedResultDto<TDto>>> fetchFromDb, TInput input, string cacheKeyBase, TimeSpan cacheDuration)
    {
        var serializedInput = JsonSerializer.Serialize(input);

        var cacheKey = input.GenerateCacheKey(cacheKeyBase);

        var cachedDataString = await distributedCache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedDataString))
        {
            logger.LogInformation($"Cache hit for key: {cacheKey}");
            var cachedData = JsonSerializer.Deserialize<PagedResultDto<TDto>>(cachedDataString);
            return cachedData!;
        }

        var result = await fetchFromDb();

        var serializedData = JsonSerializer.Serialize(result);
        await distributedCache.SetStringAsync(
            cacheKey,
            serializedData,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration
            });

        logger.LogInformation($"Cache set for key: {cacheKey}");
        return result;
    }


    //Lookupların cache işlemleri için ayrı bir metot yazıldı.
    public async Task<PagedResultDto<Shared.LookupDto<TKey>>> GetOrAddLookupAsync<TKey>(Func<Task<PagedResultDto<Shared.LookupDto<TKey>>>> fetchFromDb, TInput input, string cacheKeyBase, TimeSpan cacheDuration)
    {
        var cacheKey = input.GenerateCacheKey(cacheKeyBase);
        var cachedDataString = await distributedCache.GetStringAsync(cacheKey);

        if (!string.IsNullOrEmpty(cachedDataString))
        {
            logger.LogInformation($"Cache hit for key: {cacheKey}");
            var cachedData = JsonSerializer.Deserialize<PagedResultDto<LookupDto<TKey>>>(cachedDataString);
            return cachedData!;
        }

        var result = await fetchFromDb();
        var serializedData = JsonSerializer.Serialize(result);
        await distributedCache.SetStringAsync(
            cacheKey,
            serializedData,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = cacheDuration
            });

        logger.LogInformation($"Cache set for key: {cacheKey}");
        return result;
    }
}

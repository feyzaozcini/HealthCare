using System.Text.Json;

namespace Pusula.Training.HealthCare.Core.Helpers.Concretes;

public static class CacheKeyExtensions
{
    public static string GenerateCacheKey<TInput>(this TInput input, string cacheKeyBase)
    {
        var serializedInput = JsonSerializer.Serialize(input);
        return $"{cacheKeyBase}:{serializedInput}";
    }
}

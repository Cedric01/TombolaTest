using Microsoft.Extensions.Caching.Distributed;

namespace CoffeeBeansApi.Helpers;

public static class CacheHelper
{
    public static DistributedCacheEntryOptions GetCacheOptions(int minutes = 60)
    {
        return new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
        };
    }
}

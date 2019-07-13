using System;
using System.Runtime.Caching;

namespace DB2019.Backend.Api.Helpers
{
    public static class InMemoryCache //: ICacheService
    {
        public static T GetOrSet<T>(string cacheKey, Func<T> getItemCallback, int seconds = 300) where T : class
        {
            T item = MemoryCache.Default.Get(cacheKey) as T;
            if (item == null)
            {
                item = getItemCallback();
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddSeconds(seconds));
            }
            return item;
        }
    }
}
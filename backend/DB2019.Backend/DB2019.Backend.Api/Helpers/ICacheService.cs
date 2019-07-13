using System;

namespace DB2019.Backend.Api.Helpers
{
    interface ICacheService
    {
        T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class;
    }
}
using System;
using Microsoft.Extensions.Caching.Memory;

namespace GildedRose.Cache;

internal static class CacheItem
{
    internal static readonly MemoryCache cache = new(new MemoryCacheOptions()
    {
        ExpirationScanFrequency = TimeSpan.FromMinutes(30)
    });
}

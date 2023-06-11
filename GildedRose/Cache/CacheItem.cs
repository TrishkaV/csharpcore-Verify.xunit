using System;
using System.Collections.Generic;
using GildedRose.DataLayer;
using GildedRose.Results;
using GildedRoseKata;
using Microsoft.Extensions.Caching.Memory;

namespace GildedRose.Cache;

internal static class CacheItem
{
    private static readonly MemoryCache cache = new(new MemoryCacheOptions()
    {
        ExpirationScanFrequency = TimeSpan.FromMinutes(30)
    });

    public static bool Remove(object key)
    {
        cache.Remove(key);
        return true;
    }

    public static bool Clear()
    {
        cache.Clear();
        return true;
    }

    internal static ItemType[] GetItemTypes(IDataLayer? dl = null)
    {
        var name = "itemtypes";

        if (!cache.TryGetValue(name, out ItemType[]? items) && dl != null)
        {
            items = dl.GetItemTypes();
            cache.Set(name, items);
        }

        if (items == null || items.Length == 0)
        {
            Console.WriteLine("ERR: it was not possible to retrieve Item types.");
            if (dl == null)
                Console.WriteLine("\tno datalayer connection was provided so the database was not queried.");
            return Array.Empty<ItemType>();
        }

        return items;
    }

    internal static int GetCommonItemMaxQuality(IDataLayer? dl = null)
    {
        var name = "CommonItemMaxQuantity";

        if (!cache.TryGetValue(name, out int? val) && dl != null)
        {
            val = dl.GetCommonItemMaxQuality();
            cache.Set(name, val.Value);
        }

        if (val == null)
        {
            Console.WriteLine("ERR: the max quality for a common item could not be found.");
            if (dl == null)
                Console.WriteLine("\tno datalayer connection was provided so the database was not queried.");
        }

        return val!.Value;
    }

    internal static int GetCommonConjuredQualityDegradeFactor(IDataLayer? dl = null)
    {
        var name = "CommonConjuredQualityDegradeFactor";

        if (!cache.TryGetValue(name, out int? val) && dl != null)
        {
            val = dl.GetCommonConjuredQualityDegradeFactor();
            cache.Set(name, val);
        }

        if (val == null)
        {
            Console.WriteLine("ERR: the quality degrade factor for a common conjured item could not be found.");
            if (dl == null)
                Console.WriteLine("\tno datalayer connection was provided so the database was not queried.");
        }

        return val!.Value;
    }

    internal static int GetLegendarySulfurasFixQuality(IDataLayer? dl = null)
    {
        var name = "LegendarySulfurasFixQuality";

        if (!cache.TryGetValue(name, out int? val) && dl != null)
        {
            val = dl.GetLegendarySulfurasFixQuality();
            cache.Set(name, val);
        }

        if (val == null)
        {
            Console.WriteLine("ERR: the quality for a Sulfuras Legendary item could not be found.");
            if (dl == null)
                Console.WriteLine("\tno datalayer connection was provided so the database was not queried.");
        }

        return val!.Value;
    }

    internal static List<Item> GetItemsByType(ItemType name, IDataLayer? dl = null)
    {
        if (!cache.TryGetValue(name, out List<Item>? items) && dl != null)
        {
            items = name switch
            {
                ItemType.CommonAppreciate => dl.GetItemsCommonAppreciate(),
                ItemType.CommonDepreciate => dl.GetitemsCommonDepreciate(),
                ItemType.CommonConjured => dl.GetitemsCommonConjured(),
                ItemType.CommonBackstagePass => dl.GetitemsCommonBackstagePass(),
                ItemType.Legendary => dl.GetItemsLegendary(),
                _ => throw new ArgumentException($"Unhandled enum value: {name}")
            };

            cache.Set(name, items);
        }

        if (items == null || items.Count == 0)
        {
            Console.WriteLine($"ERR: no items of type \"{name}\" exist.");
            if (dl == null)
                Console.WriteLine("\tno datalayer connection was provided so the database was not queried.");
            return new List<Item>();
        }

        return items;
    }
}

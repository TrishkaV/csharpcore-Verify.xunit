using System;
using System.Linq;
using GildedRose.Cache;
using GildedRose.DataLayer;
using GildedRose.Results;
using GildedRoseKata;

namespace GildedRose.Extensions;

public static class ExtItem
{
    public static bool ItemEquals(this Item item, object? obj)
    {
        if (obj == null || obj is not Item)
            return false;
        var input = (Item)obj;

        return input.Name == item.Name;
    }

    public static ItemType GetItemType(this Item item, IDataLayer? dataLayer = null)
    {
        foreach (var t in CacheItem.GetItemTypes(dataLayer))
        {
            var items = CacheItem.GetItemsByType(t, dataLayer);
            if (items.Any(x => x.ItemEquals(item)))
                return t;
        }

        Console.WriteLine($"WARN: Item \"{item.Name}\" does not belong to any item class. Defaulting to Common Depreciable.");
        return ItemType.CommonDepreciate;
    }
}

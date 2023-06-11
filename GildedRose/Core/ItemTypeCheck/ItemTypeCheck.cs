using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Extensions;
using GildedRose.Results;
using GildedRoseKata;

namespace GildedRose.Core.ItemTypeCheck;

public static class ItemTypeCheck
{
    internal static bool IsLegendarySulfuras(Item item)
    {
        return item.Name == "Sulfuras, Hand of Ragnaros";
    }

    public static ItemType GetItemType(Item item, List<Item>[] itemsAll)
    {
        foreach (var ia in itemsAll)
            if (ia.Any(x => x.ItemEquals(item)))
                return (ItemType)Enum.Parse(typeof(ItemType), nameof(ia).Replace("items", string.Empty));

        Console.WriteLine($"WARN: Item \"{item.Name}\" does not belong to any item class. Defaulting to Common Depreciable.");
        return ItemType.CommonDepreciate;
    }
}

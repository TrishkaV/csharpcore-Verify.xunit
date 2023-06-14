using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Cache;
using GildedRose.Core;
using GildedRose.Core.ItemTypeCheck;
using GildedRose.DataLayer;
using GildedRose.Extensions;
using GildedRose.Results;

namespace GildedRoseKata;

public class GildedRose
{
    readonly IList<Item> Items;

    private readonly IDataLayer dataLayer;

    private readonly int commonMaxQuality;
    private readonly int commonConjuredQualityDegradeFactor;
    private readonly int legendarySulfurasFixQuality;

    public GildedRose(IList<Item> Items, IDataLayer? dl = null)
    {
        dataLayer = dl ?? new DatabaseMock();

        commonMaxQuality = CacheItem.GetCommonItemMaxQuality(dataLayer);
        commonConjuredQualityDegradeFactor = CacheItem.GetCommonConjuredQualityDegradeFactor(dataLayer);
        legendarySulfurasFixQuality = CacheItem.GetLegendarySulfurasFixQuality(dataLayer);

        ConstructorGuard(Items);

        this.Items = Items;
    }

    ///<summary>
    ///<para>Enforce through removal:</para>
    ///<para>- Item name cannot be empty</para>
    ///<para>Enforce through modification:</para>
    ///<para>- Sulfuras fixed quality</para>
    ///<para>- Common items max quality</para>
    ///</summary>
    private bool ConstructorGuard(IList<Item> Items)
    {
        for (var i = Items.Count - 1; i >= 0; i--)
        {
            if (string.IsNullOrWhiteSpace(Items[i].Name))
            {
                Console.WriteLine("An Item must have a name, it will be excluded.");
                Items.RemoveAt(i);
                continue;
            }

            if (ItemTypeCheck.IsLegendarySulfuras(Items[i]) && Items[i].Quality != legendarySulfurasFixQuality)
            {
                Console.WriteLine($"A \"Sulfuras, Hand of Ragnaros\" item with a quality of {Items[i].Quality} was passed, restoring to {legendarySulfurasFixQuality}.");
                Items[i].Quality = legendarySulfurasFixQuality;
            }
            else if (ExtEnum.SelectMultipleByName<ItemType>("Common").Contains(Items[i].GetItemType(dataLayer))
                && Items[i].Quality > commonMaxQuality)
            {
                Console.WriteLine($"Item \"{Items[i].Name}\" was passed with a quality of {Items[i].Quality} while the max quality for a common item is {commonMaxQuality}, the max quality will be used.");
                Items[i].Quality = commonMaxQuality;
            }
        }

        return true;
    }

    public void UpdateQuality()
    {
        foreach (var i in Items)
        {
            var itemType = i.GetItemType(dataLayer);
            var _ = itemType switch
            {
                ItemType.CommonAppreciate => ItemQualityUpdate.ItemCommonAppreciate(i, commonMaxQuality),
                ItemType.CommonDepreciate => ItemQualityUpdate.ItemCommonDepreciate(i),
                ItemType.CommonConjured => ItemQualityUpdate.ItemCommonConjuredDepreciate(i, commonConjuredQualityDegradeFactor),
                ItemType.CommonBackstagePass => ItemQualityUpdate.ItemCommonBackstagePassDepreciate(i),
                ItemType.Legendary => ItemQualityUpdate.ItemLegendaryDepreciate(i),
                _ => throw new ArgumentException($"Unhandled enum value: {itemType}")
            };
        }
    }
}

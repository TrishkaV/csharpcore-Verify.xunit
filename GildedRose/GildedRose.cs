using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Cache;
using GildedRose.DataLayer;
using GildedRose.Results;
using Microsoft.Extensions.Caching.Memory;

namespace GildedRoseKata;

public partial class GildedRose
{
    readonly IList<Item> Items;

    private readonly IDataLayer dataLayer;

    private readonly List<string> itemsCommonAppreciate;
    private readonly List<string> itemsCommonDepreciate;
    private readonly List<string> itemsCommonConjured;
    private readonly List<string> itemsCommonBackstagePass;
    private readonly List<string> itemsLegendary;

    private readonly int commonItemMaxQuality;
    private readonly int commonConjuredQualityDegradeFactor;

    public GildedRose(IList<Item> Items, IDataLayer? dl = null)
    {
        dataLayer = dl ?? new DatabaseMock();

        #region assign local items
#pragma warning disable CS8601 // assigning null value to non nullable variable: handled within the logic 
        if (!CacheItem.cache.TryGetValue("CommonAppreciate", out itemsCommonAppreciate) || itemsCommonAppreciate == null)
        {
            itemsCommonAppreciate = dataLayer.GetItemsCommonAppreciate();
            CacheItem.cache.Set("CommonAppreciate", itemsCommonAppreciate);
        }
        if (!CacheItem.cache.TryGetValue("CommonDepreciate", out itemsCommonDepreciate) || itemsCommonDepreciate == null)
        {
            itemsCommonDepreciate = dataLayer.GetitemsCommonDepreciate();
            CacheItem.cache.Set("CommonDepreciate", itemsCommonAppreciate);
        }
        if (!CacheItem.cache.TryGetValue("CommonConjured", out itemsCommonConjured) || itemsCommonConjured == null)
        {
            itemsCommonConjured = dataLayer.GetitemsCommonConjured();
            CacheItem.cache.Set("CommonConjured", itemsCommonConjured);
        }
        if (!CacheItem.cache.TryGetValue("CommonBackstagePass", out itemsCommonBackstagePass) || itemsCommonBackstagePass == null)
        {
            itemsCommonBackstagePass = dataLayer.GetitemsCommonBackstagePass();
            CacheItem.cache.Set("CommonBackstagePass", itemsCommonBackstagePass);
        }
        if (!CacheItem.cache.TryGetValue("Legendary", out itemsLegendary) || itemsLegendary == null)
        {
            itemsLegendary = dataLayer.GetItemsLegendary();
            CacheItem.cache.Set("Legendary", itemsLegendary);
        }
        if (!CacheItem.cache.TryGetValue("CommonMaxQuality", out commonItemMaxQuality) || commonItemMaxQuality == 0)
        {
            commonItemMaxQuality = dataLayer.GetCommonItemMaxQuality();
            CacheItem.cache.Set("CommonMaxQuality", commonItemMaxQuality);
        }
        if (!CacheItem.cache.TryGetValue("CommonConjuredQualityDegradeFactor", out commonConjuredQualityDegradeFactor) || commonConjuredQualityDegradeFactor == 0)
        {
            commonConjuredQualityDegradeFactor = dataLayer.GetCommonConjuredQualityDegradeFactor();
            CacheItem.cache.Set("CommonConjuredQualityDegradeFactor", commonConjuredQualityDegradeFactor);
        }
#pragma warning restore CS8601
        #endregion

        #region handle logic breaking items
        for (var i = Items.Count - 1; i > 0; i--)
        {
            if (string.IsNullOrWhiteSpace(Items[i].Name))
            {
                Console.WriteLine("An Item must have a name, it will be excluded.");
                Items.RemoveAt(i);
            }

            if (Items[i].Name == "Sulfuras, Hand of Ragnaros" && Items[i].Quality != 80)
            {
                Console.WriteLine($"A \"Sulfuras, Hand of Ragnaros\" item with a quality of {Items[i].Quality} was passed, restoring to 80.");
                Items[i].Quality = 80;
            }
            else if (new ItemType[] { ItemType.CommonAppreciate, ItemType.CommonDepreciate, ItemType.CommonConjured, ItemType.CommonBackstagePass }.Contains(GetItemType(Items[i]))
                && Items[i].Quality > commonItemMaxQuality)
            {
                Console.WriteLine($"Item \"{Items[i].Name}\" was passed with a quality of {Items[i].Quality} while the max quality for a common item is {commonItemMaxQuality}, the max quality will be used.");
                Items[i].Quality = commonItemMaxQuality;
            }
        }
        #endregion

        this.Items = Items;
    }

    public void UpdateQuality()
    {
        foreach (var i in Items)
        {
            switch (GetItemType(i))
            {
                case ItemType.CommonAppreciate:
                    ItemCommonAppreciate(i);
                    break;
                case ItemType.CommonDepreciate:
                    ItemCommonDepreciate(i);
                    break;
                case ItemType.CommonConjured:
                    ItemCommonConjuredDepreciate(i);
                    break;
                case ItemType.CommonBackstagePass:
                    ItemCommonBackstagePassDepreciate(i);
                    break;
                case ItemType.Legendary:
                    ItemLegendaryDepreciate(i);
                    break;
            }
        }
    }

    private ItemType GetItemType(Item item)
    {
        if (itemsCommonAppreciate.Contains(item.Name))
            return ItemType.CommonAppreciate;
        else if (itemsCommonDepreciate.Contains(item.Name))
            return ItemType.CommonDepreciate;
        else if (itemsCommonConjured.Contains(item.Name))
            return ItemType.CommonConjured;
        else if (itemsCommonBackstagePass.Contains(item.Name))
            return ItemType.CommonBackstagePass;
        else if (itemsLegendary.Contains(item.Name))
            return ItemType.Legendary;

        Console.WriteLine($"WARN: Item \"{item.Name}\" does not belong to any item class. Defaulting to Common Depreciable.");
        return ItemType.CommonDepreciate;
    }
}

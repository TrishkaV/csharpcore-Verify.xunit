using System;
using System.Collections.Generic;
using System.Linq;
using GildedRose.Extensions;
using GildedRose.Results;
using GildedRoseKata;

namespace GildedRose.DataLayer;

internal sealed class DatabaseMock : IDataLayer
{
    ItemType[] IDataLayer.GetItemTypes()
    {
        var dbResponse = new string[] { "CommonAppreciate", "CommonDepreciate", "CommonBackstagePass", "CommonConjured", "Legendary" };

        var itemTypeAll = new List<ItemType>();
        foreach (var r in dbResponse)
        {
            if (!Enum.TryParse(r, ignoreCase: true, out ItemType itemType))
            {
                Console.WriteLine($"ERR: Item type \"{r}\" returned from database is not mapped, will not be used.");
                continue;
            }

            itemTypeAll.Add(itemType);
        }

        return itemTypeAll.ToArray();
    }

    List<Item> IDataLayer.GetItemsCommonAppreciate()
    {
        return new List<Item>()
        {
            new Item {Name = "Aged Brie", SellIn = 2, Quality = 0}
        };
    }

    List<Item> IDataLayer.GetitemsCommonDepreciate()
    {
        return new List<Item>()
        {
            new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
            new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7}
        };
    }

    List<Item> IDataLayer.GetitemsCommonBackstagePass()
    {
        return new List<Item>()
        {
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 15,
                Quality = 20
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 10,
                Quality = 49
            },
            new Item
            {
                Name = "Backstage passes to a TAFKAL80ETC concert",
                SellIn = 5,
                Quality = 49
            }
        };
    }

    int IDataLayer.GetCommonItemMaxQuality()
    {
        return 50;
    }
    int IDataLayer.GetCommonConjuredQualityDegradeFactor()
    {
        return 2;
    }

    List<Item> IDataLayer.GetitemsCommonConjured()
    {
        return new List<Item>()
        {
            new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
        };
    }

    List<Item> IDataLayer.GetItemsLegendary()
    {
        return new List<Item>()
        {
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
            new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80}
        };
    }

    int IDataLayer.GetLegendarySulfurasFixQuality()
    {
        return 80;
    }
}

using System.Collections.Generic;

namespace GildedRose.DataLayer;

internal sealed class DatabaseMock : IDataLayer
{
    List<string> IDataLayer.GetItemsCommonAppreciate()
    {
        return new List<string>() { "Aged Brie" };
    }

    List<string> IDataLayer.GetitemsCommonDepreciate()
    {
        return new List<string>() { "+5 Dexterity Vest", "Elixir of the Mongoose" };
    }

    List<string> IDataLayer.GetitemsCommonBackstagePass()
    {
        return new List<string>() { "Backstage passes to a TAFKAL80ETC concert" };
    }

    int IDataLayer.GetCommonItemMaxQuality()
    {
        return 50;
    }
    int IDataLayer.GetCommonConjuredQualityDegradeFactor()
    {
        return 2;
    }

    List<string> IDataLayer.GetitemsCommonConjured()
    {
        return new List<string>() { "Conjured Mana Cake" };
    }

    List<string> IDataLayer.GetItemsLegendary()
    {
        return new List<string>() { "Sulfuras, Hand of Ragnaros" };
    }
}

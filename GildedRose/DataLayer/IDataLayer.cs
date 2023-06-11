using System.Collections.Generic;
using GildedRose.Results;
using GildedRoseKata;

namespace GildedRose.DataLayer;

public interface IDataLayer
{
    List<Item> GetItemsCommonAppreciate();
    List<Item> GetitemsCommonDepreciate();
    List<Item> GetitemsCommonConjured();
    List<Item> GetitemsCommonBackstagePass();
    int GetCommonItemMaxQuality();
    int GetCommonConjuredQualityDegradeFactor();
    List<Item> GetItemsLegendary();
    int GetLegendarySulfurasFixQuality();
    ItemType[] GetItemTypes();
}

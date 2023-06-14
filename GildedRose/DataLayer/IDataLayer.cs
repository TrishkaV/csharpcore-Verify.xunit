using System.Collections.Generic;
using GildedRose.Results;
using GildedRoseKata;

namespace GildedRose.DataLayer;

public interface IDataLayer
{
    #region SELECT Item Common
    List<Item> GetItemsCommonAppreciate();
    List<Item> GetitemsCommonDepreciate();
    List<Item> GetitemsCommonConjured();
    List<Item> GetitemsCommonBackstagePass();
    int GetCommonItemMaxQuality();
    int GetCommonConjuredQualityDegradeFactor();
    #endregion

    #region SELECT Item Legendary
    List<Item> GetItemsLegendary();
    int GetLegendarySulfurasFixQuality();
    #endregion

    ///<summary>
    ///<para>Get an ItemType array of the item types listed by the data source</para>
    ///</summary>
    ItemType[] GetItemTypes();
}

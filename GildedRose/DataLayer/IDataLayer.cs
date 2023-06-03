using System.Collections.Generic;

namespace GildedRose.DataLayer;

public interface IDataLayer
{
    List<string> GetItemsCommonAppreciate();
    List<string> GetitemsCommonDepreciate();
    List<string> GetitemsCommonConjured();
    List<string> GetitemsCommonBackstagePass();
    int GetCommonItemMaxQuality();
    int GetCommonConjuredQualityDegradeFactor();
    List<string> GetItemsLegendary();
}

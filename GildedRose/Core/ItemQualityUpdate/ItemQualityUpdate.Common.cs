using GildedRoseKata;

namespace GildedRose.Core;

internal static partial class ItemQualityUpdate
{
    internal static bool ItemCommonAppreciate(Item item, int commonItemMaxQuality)
    {
        if (--item.SellIn < 0)
            item.SellIn = 0; /* avoid int underflow */

        if (++item.Quality > commonItemMaxQuality)
            item.Quality = commonItemMaxQuality; /* common items have a max quality */

        return true;
    }

    internal static bool ItemCommonBackstagePassDepreciate(Item item)
    {
        /* it is mentioned that quality drops to 0 AFTER the concert, so when SellIn is 0
            (the day of the concert) then the pass still holds its value 
        */
        switch (--item.SellIn)
        {
            case var x when x < 0:
                item.SellIn = 0; /* avoid int underflow */
                item.Quality = 0;
                break;
            case var x when x < 5:
                item.Quality += 3;
                break;
            case var x when x < 10:
                item.Quality += 2;
                break;
        }

        return true;
    }

    internal static bool ItemCommonDepreciate(Item item, bool isItemConjured = false, int commonConjuredQualityDegradeFactor = 1)
    {
        var degradeFactorExpired = 1;
        var degradeFactorConjured = isItemConjured ? commonConjuredQualityDegradeFactor : 1;

        if (--item.SellIn < 0)
        {
            item.SellIn = 0; /* avoid int underflow */
            degradeFactorExpired = 2;
        }

        if ((item.Quality -= 1 * degradeFactorExpired * degradeFactorConjured) < 0) /* item quality cannot be negative */
            item.Quality = 0;

        return true;
    }

    internal static bool ItemCommonConjuredDepreciate(Item item, int commonConjuredQualityDegradeFactor)
        => ItemCommonDepreciate(item, isItemConjured: true, commonConjuredQualityDegradeFactor);
}

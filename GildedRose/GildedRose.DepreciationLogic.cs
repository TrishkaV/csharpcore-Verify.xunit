namespace GildedRoseKata;

public partial class GildedRose
{
    private bool ItemCommonAppreciate(Item item)
    {
        if (--item.SellIn < 0)
            item.SellIn = 0; /* avoid int underflow */

        if (++item.Quality > commonItemMaxQuality)
            item.Quality = commonItemMaxQuality; /* common items have a max quality */

        return true;
    }

    private static bool ItemCommonBackstagePassDepreciate(Item item)
    {
        /* it is mentioned that quality drops to 0 AFTER the concert, so when SellIn is 0
            (the day of the concert) then the pass still holds its value */
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

    private bool ItemCommonDepreciate(Item item, bool isItemConjured = false)
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

    private bool ItemCommonConjuredDepreciate(Item item)
    {
        return ItemCommonDepreciate(item, isItemConjured: true);
    }

    private static bool ItemLegendaryDepreciate(Item item)
    {
        /* legendary items do not need to be sold or depreciate, currently do nothing */

        return true;
    }
}

using GildedRoseKata;

namespace GildedRose.Core;

internal static partial class ItemQualityUpdate
{
    internal static bool ItemLegendaryDepreciate(Item item)
    {
        /* legendary items do not need to be sold or depreciate, currently do nothing */

        return true;
    }
}

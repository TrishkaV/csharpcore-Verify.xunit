using System;
using System.Collections.Generic;

namespace GildedRoseKata;
public static class Program
{
    ///<summary>
    ///<para>Argumengs:</para>
    /// <param name="args"> [0] - number of days to test</param>
    ///</summary>
    public static void Main(string[]? args = null)
    {
        if (!int.TryParse(args[0], out int nDays))
            nDays = 30;

        Console.WriteLine("OMGHAI!");

        IList<Item> Items = new List<Item>{
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -1, Quality = 80},
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
                },
#if DEBUG
				// this conjured item does not work properly yet
				new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
#endif
            };

        var app = new GildedRose(Items);

        for (var i = 1; i <= nDays; i++)
        {
            Console.WriteLine("-------- day " + i + " --------");
            Console.WriteLine("name, sellIn, quality");
            for (var j = 0; j < Items.Count; j++)
            {
                Console.WriteLine($"{Items[j].Name}, {Items[j].SellIn}, {Items[j].Quality}");
            }
            Console.WriteLine("");
            app.UpdateQuality();
        }
    }
}

using System.Collections.Generic;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests
{
    public class GildedRoseTest
    {
        [Fact]
        public void Foo()
        {
            var Items = new List<Item> { new Item { Name = "Foo", SellIn = 0, Quality = 0 } };
            var app = new GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal("Foo", Items[0].Name);
        }
    }
}

using System.Collections.Generic;
using GildedRoseKata;
using Xunit;

namespace GildedRoseTests;

public class GildedRoseTest
{
    [Fact]
    public void Foo()
    {
        var Items = new List<Item> { new Item { Name = "Foo", SellIn = 0, Quality = 0 } };
        var app = new GildedRoseKata.GildedRose(Items);
        app.UpdateQuality();
        Assert.Equal("Foo", Items[0].Name);
    }

    [Fact]
    public void ItemNonValid()
    {
        var Items = new List<Item> { new Item { Name = "", SellIn = 10, Quality = 10 } };
        _ = new GildedRoseKata.GildedRose(Items);
        Assert.Empty(Items);
    }

    public class ItemsCommonTests
    {
        [Fact]
        public void CommonDepreciateOne()
        {
            var Items = new List<Item> { new Item { Name = "Foo", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            app.UpdateQuality();
            Assert.Equal(9, Items[0].Quality);
            Assert.Equal(9, Items[0].SellIn);
        }

        [Fact]
        public void CommonDepreciateTen()
        {
            var Items = new List<Item> { new Item { Name = "Foo", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 10; i++)
                app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
            Assert.Equal(0, Items[0].SellIn);
        }

        [Fact]
        public void CommonAppreciate()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 10; i++)
                app.UpdateQuality();
            Assert.Equal(20, Items[0].Quality);
        }

        [Fact]
        public void CommonAppreciateAboveQuality50()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 100; i++)
                app.UpdateQuality();
            Assert.Equal(50, Items[0].Quality);
        }

        [Fact]
        public void CommonAppreciateBeyondExpiry()
        {
            var Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 20; i++)
                app.UpdateQuality();
            Assert.Equal(30, Items[0].Quality);
        }

        [Fact]
        public void CommonConjuredDepreciate()
        {
            var Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 5; i++)
                app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }

        [Fact]
        public void CommonConjuredDepreciateBeyondExpiry()
        {
            var Items = new List<Item> { new Item { Name = "Conjured Mana Cake", SellIn = 10, Quality = 10 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 20; i++)
                app.UpdateQuality();
            Assert.Equal(0, Items[0].Quality);
        }
    }

    public class ItemsLegendaryTests
    {
        [Fact]
        public void LegendarySulfurasDepreciate()
        {
            var Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 10, Quality = 80 } };
            var app = new GildedRoseKata.GildedRose(Items);
            for (var i = 0; i < 20; i++)
                app.UpdateQuality();
            Assert.Equal(80, Items[0].Quality);
        }
    }
}

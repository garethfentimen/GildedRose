using Xunit;

namespace GildedRose.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using GildedRose.Console;

    public class TestAssemblyTests
    {
        [Fact]
        public void TestWhenTheSellInDateHasPassedQualityDegradesByOne()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 2, Quality = 20 } } };
            guildedRose.UpdateQuality();
            Assert.Equal(19, guildedRose.Items.First().Quality);
            Assert.Equal(1, guildedRose.Items.First().SellIn);
        }

        [Fact]
        public void TestWhenTheSellInDateHasPassedQualityDegradesTwiceAsFast()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 2, Quality = 20 } } };
            this.RunUpdateQualityXTimes(2, guildedRose);
            Assert.Equal(18, guildedRose.Items.First().Quality);
            Assert.Equal(0, guildedRose.Items.First().SellIn);
            guildedRose.UpdateQuality();
            Assert.Equal(16, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void QualityCanNeverBeNegative()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "+5 Dexterity Vest", SellIn = 2, Quality = 3 } } };
            this.RunUpdateQualityXTimes(2, guildedRose);
            Assert.Equal(1, guildedRose.Items.First().Quality);
            Assert.Equal(0, guildedRose.Items.First().SellIn);
            guildedRose.UpdateQuality();
            Assert.Equal(0, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void AgedBrieIncreasesInQuality()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 5, Quality = 5 } } };
            this.RunUpdateQualityXTimes(2, guildedRose);
            Assert.True(guildedRose.Items.First().Quality == 7);
            Assert.Equal(3, guildedRose.Items.First().SellIn);
            guildedRose.UpdateQuality();
            Assert.True(guildedRose.Items.First().Quality == 8);
        }

        [Fact]
        public void TheQualityIsNeverLargerThan50()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Aged Brie", SellIn = 60, Quality = 1 } } };
            this.RunUpdateQualityXTimes(51, guildedRose);
            Assert.True(guildedRose.Items.First().Quality == 50);
            Assert.Equal(9, guildedRose.Items.First().SellIn);
        }

        [Fact]
        public void SulfurasNeverHasToBeSold()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } } };
            this.RunUpdateQualityXTimes(51, guildedRose);
            Assert.True(guildedRose.Items.First().SellIn == 0);
        }

        [Fact]
        public void SulfurasNeverDecreasesInQuality()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80 } } };
            this.RunUpdateQualityXTimes(51, guildedRose);
            Assert.Equal(80, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void BackstagePassesIncreaseInQualityByTwoWhen10DaysOrLessToConcert()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } } };
            this.RunUpdateQualityXTimes(5, guildedRose);
            Assert.Equal(25, guildedRose.Items.First().Quality);
            this.RunUpdateQualityXTimes(1, guildedRose);
            Assert.Equal(27, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void BackstagePassesIncreaseInQualityThriceFoldWhen5DaysOrLessToConcert()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } } };
            this.RunUpdateQualityXTimes(5, guildedRose);
            this.RunUpdateQualityXTimes(5, guildedRose);
            Assert.Equal(35, guildedRose.Items.First().Quality);
            this.RunUpdateQualityXTimes(1, guildedRose);
            Assert.Equal(38, guildedRose.Items.First().Quality);
            this.RunUpdateQualityXTimes(4, guildedRose);
            Assert.Equal(50, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void BackstagePassesDecreaseInQualityToZeroOnceConcertHappens()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Backstage passes to a TAFKAL80ETC concert", SellIn = 15, Quality = 20 } } };
            this.RunUpdateQualityXTimes(15, guildedRose);
            Assert.Equal(50, guildedRose.Items.First().Quality);
            this.RunUpdateQualityXTimes(1, guildedRose);
            Assert.Equal(0, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void ConjuredItemsDegradeTwiceAsFastAsNormalItems()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Conjured ham", SellIn = 5, Quality = 20 } } };
            this.RunUpdateQualityXTimes(1, guildedRose);
            Assert.Equal(18, guildedRose.Items.First().Quality);
        }

        [Fact]
        public void ConjuredItemsDegradeTwiceAsFastAsNormalItemsWhenPastSellByDate()
        {
            var guildedRose = new GuildedRoseManager { Items = new List<Item> { new Item { Name = "Conjured ham", SellIn = 5, Quality = 20 } } };
            this.RunUpdateQualityXTimes(5, guildedRose);
            Assert.Equal(10, guildedRose.Items.First().Quality);
            this.RunUpdateQualityXTimes(1, guildedRose);
            Assert.Equal(6, guildedRose.Items.First().Quality);
        }

        private void RunUpdateQualityXTimes(int timesToRun, GuildedRoseManager guildedRoseManager)
        {
            for (var i = 0; i < timesToRun; i++)
            {
                guildedRoseManager.UpdateQuality();    
            }
        }
    }


}
namespace GildedRose.Console
{
    using System.Collections.Generic;

    public class GuildedRoseManager
    {
        public IList<Item> Items;

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                if (Items[i].Name != "Aged Brie" && Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                {
                    if (Items[i].Quality > 0)
                    {
                        if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            Items[i].Quality = Items[i].Quality - this.checkForConjured(1, Items[i].Name);
                        }
                    }
                }
                else
                {
                    if (Items[i].Quality < 50)
                    {
                        Items[i].Quality = Items[i].Quality + 1;

                        if (Items[i].Name == "Backstage passes to a TAFKAL80ETC concert")
                        {
                            if (Items[i].SellIn < 11)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }

                            if (Items[i].SellIn < 6)
                            {
                                if (Items[i].Quality < 50)
                                {
                                    Items[i].Quality = Items[i].Quality + 1;
                                }
                            }
                        }
                    }
                }

                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                {
                    Items[i].SellIn = Items[i].SellIn - 1;
                }

                if (this.Items[i].SellIn >= 0)
                {
                    continue;
                }

                if (this.Items[i].Name != "Aged Brie")
                {
                    if (this.Items[i].Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (this.Items[i].Quality <= 0)
                        {
                            continue;
                        }
                        if (this.Items[i].Name != "Sulfuras, Hand of Ragnaros")
                        {
                            this.Items[i].Quality = this.Items[i].Quality - this.checkForConjured(1, this.Items[i].Name);
                        }
                    }
                    else
                    {
                        this.Items[i].Quality = this.Items[i].Quality - this.Items[i].Quality;
                    }
                }
                else
                {
                    if (this.Items[i].Quality < 50)
                    {
                        this.Items[i].Quality = this.Items[i].Quality + 1;
                    }
                }
            }
        }

        private int checkForConjured(int incrementalValue, string itemName)
        {
            if (itemName.StartsWith("Conjured"))
            {
                return incrementalValue * 2;    
            }
            return incrementalValue;
        }
    }
}
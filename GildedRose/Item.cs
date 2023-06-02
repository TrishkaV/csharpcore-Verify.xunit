namespace GildedRoseKata;

public class Item
{
    private int quality;

    public string? Name { get; set; }

    public int SellIn { get; set; }

    public int Quality
    {
        get { return quality; }
        set
        {
            if (Name != null && Name == "Sulfuras")
            {
                quality = 80; // Sulfuras is legendary and its quality is always 80
            }
            else
            {
                quality = value > 50 ? 50 : value; // Quality capped at 50
            }
        }
    }
}

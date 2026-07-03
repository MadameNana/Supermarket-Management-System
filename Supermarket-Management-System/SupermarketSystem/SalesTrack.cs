using System;

//Item storing class for the supermarket system
public class SalesTrack
{
    public string saleID { get; set; }
    public string saleDate { get; set; }
    public int saleAmount { get; private set; }

    private SoldStore[] items = new SoldStore[5];
    private int itemAmount = 0;

    public SalesTrack(string saleID, string saleDate)
    {
        this.saleID = saleID;
        this.saleDate = saleDate;
        this.saleAmount = 0;
    }

    //-------------------------------
    //--Sale Method
    public void ItemSold(SoldStore item)
    {
        if (itemAmount == items.Length)
        {
            Array.Resize(ref items, items.Length * 2);
        }

        items[itemAmount] = item;
        itemAmount++;

        saleAmount += item.salePrice * item.amountSold;
    }

    //-------------------------------
    //--Report Method
    public void SaleReport()
    {
        Console.WriteLine("Sale Report:");
        Console.WriteLine($" Sale ID: {saleID}  \n Date Sold: '{saleDate}' \n Amount Sold: '{saleAmount}' \n --------------------");
        Console.WriteLine(" ");

        for (int x = 0; x < itemAmount; x++)
        {
            items[x].SoldReport();
        }

        Console.WriteLine();
    }
}
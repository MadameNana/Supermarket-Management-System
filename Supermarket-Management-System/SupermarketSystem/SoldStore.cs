using System;

//Sold items class for individual sales
public class SoldStore
{
    public string soldItemID { get; set; }
    public string saleID { get; set; }
    public string prodID { get; set; }
    public int amountSold { get; set; }
    public int salePrice { get; set; }

    public SoldStore(string soldItemID, string saleID, string prodID, int amountSold, int salePrice)
    {
        this.soldItemID = soldItemID;
        this.saleID = saleID;
        this.prodID = prodID;
        this.amountSold = amountSold;
        this.salePrice = salePrice;
    }



    //-------------------------------
    //--Report Method
    public void SoldReport()
    {
        Console.WriteLine("Sold Item:");
        Console.WriteLine($" ID: {prodID}  \n Amount: '{amountSold}' \n Price at Sale: '{salePrice}' \n --------------------");
        Console.WriteLine(" ");
    }

}
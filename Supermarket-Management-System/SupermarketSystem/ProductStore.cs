using System;

public class ProductStore
{
    //Item info 
    public string prodID { get; set; }
    public string prodName { get; set; }
    public string prodBrand { get; set; }
    public string prodCategory { get; set; }
    public int prodPrice { get; set; }
    public int prodStock { get; set; }
    public string prodExpiration { get; set; }

    public ProductStore(string prodID, string prodName, string prodBrand, string prodCategory, int prodPrice, int prodStock, string prodExpiration)
    {
        this.prodID = prodID;
        this.prodName = prodName;
        this.prodBrand = prodBrand;
        this.prodCategory = prodCategory;
        this.prodPrice = prodPrice;
        this.prodStock = prodStock;
        this.prodExpiration = prodExpiration;
    }

    public void Report()
    {
        Console.WriteLine($"Item report: {prodID} {prodName} {prodBrand} {prodCategory} {prodPrice} {prodStock} {prodExpiration}");
    }
}
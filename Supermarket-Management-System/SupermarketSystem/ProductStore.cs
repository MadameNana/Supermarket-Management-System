using System;

//Item storing class for the supermarket system
public class ProductStore
{
    public string prodID { get; set; }
    public string prodName { get; set; }
    public string prodSupplier { get; set; }
    public string prodCategory { get; set; }
    public int prodPrice { get; set; }
    public int prodStock { get; set; }
    public string prodExpiration { get; set; }

    public ProductStore(string prodID, string prodName, string prodSupplier, string prodCategory, int prodPrice, int prodStock, string prodExpiration)
    {
        this.prodID = prodID;
        this.prodName = prodName;
        this.prodSupplier = prodSupplier;
        this.prodCategory = prodCategory;
        this.prodPrice = prodPrice;
        this.prodStock = prodStock;
        this.prodExpiration = prodExpiration;
    }



//-------------------------------
//--Report Method
    public void ProductReport()
    {
        Console.WriteLine("Item:");
        Console.WriteLine($" ID: {prodID}  \n Name: '{prodName}' \n Supplier: '{prodSupplier}' \n Category: '{prodCategory}' \n Price: '{prodPrice}' \n Stock: '{prodStock}' \n Expr-Date: '{prodExpiration}' \n --------------------");
        Console.WriteLine(" ");
    }

}
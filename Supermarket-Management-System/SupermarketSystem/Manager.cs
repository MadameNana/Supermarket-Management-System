using System;

//-------------------------------------------------------------------
//--Sales Management class
public class SalesManager
{
    private SalesTrack[] sales = new SalesTrack[5];
    private int amount = 0;

    //Prompts user to input and save multi-item sales
    public void AddSale(Inventory inven)
    {
        Console.Write("Sale ID?: ");
        string saleID = Console.ReadLine() ?? "";

        Console.Write("Sale Date?: ");
        string saleDate = Console.ReadLine() ?? "";

        var sale = new SalesTrack(saleID, saleDate);


        bool addItems = true;
        while (addItems)
        {
            Console.Write("Sold product ID?: ");
            string prodID = Console.ReadLine() ?? "";

            Console.Write("Amount?: ");
            int.TryParse(Console.ReadLine(), out int amt);

            Console.Write("Sold for?: ");
            int.TryParse(Console.ReadLine(), out int price);

            bool fineStock = inven.LowerStock(prodID, amt);

            if (fineStock)
            {
                var item = new SoldStore("SI" + Guid.NewGuid().ToString("N").Substring(0, 5), saleID, prodID, amt, price);
                sale.ItemSold(item);
            }
            else
            {
                Console.WriteLine("Item not sold.");
            }
            Console.Write("More Sales? (y/n): ");
            string more = Console.ReadLine() ?? "";
            if (more.ToLower() != "y")
            {
                addItems = false;
            }
        }
            if (amount == sales.Length)
            {
            Array.Resize(ref sales, sales.Length * 2);
            }
        sales[amount] = sale;
        amount++;
        Console.WriteLine(" ");
        Console.WriteLine("Sale recorded.");
        Console.WriteLine(" ");
        Console.WriteLine("-----------------------------------");
    }


    public void ReportAll()
    {
        for (int x = 0; x < amount; x++)
        {
            sales[x].SaleReport();
        }
    }
}

//-------------------------------------------------------------------
//Category Management class
public class CategoryManager
{
    private CategoryStore[] categories = new CategoryStore[5];
    private int amount = 0;

    //Prompts user to add product category
    public void AddCategory()
    {
        Console.Write("Category ID?: ");
        string cateID = Console.ReadLine() ?? "";

        Console.Write("Category Name?: ");
        string cateName = Console.ReadLine() ?? "";

        if (amount == categories.Length)
        {
            Array.Resize(ref categories, categories.Length * 2);
        }

        categories[amount] = new CategoryStore(cateID, cateName);
        amount++;

        Console.WriteLine($"Category '{cateName}' added.");
    }

    public void ReportAll()
    {
        for (int x = 0; x < amount; x++)
        {
            categories[x].ReportCategories();
        }
    }
}


//-------------------------------------------------------------------
//Supplier Management class
public class SupplierManager
{
    private SupplierStore[] suppliers = new SupplierStore[5];
    private int amount = 0;

    //Prompts user to add new supplier
    public void AddSup()
    {
        Console.Write("Supplier ID?: ");
        string idSup = Console.ReadLine() ?? "";

        Console.Write("Supplier Name?: ");
        string nameSup = Console.ReadLine() ?? "";

        Console.Write("Contact Info?: ");
        string contactSup = Console.ReadLine() ?? "";

        if (amount == suppliers.Length)
        {
            Array.Resize(ref suppliers, suppliers.Length * 2);
        }

        suppliers[amount] = new SupplierStore(idSup, nameSup, contactSup);
        amount++;

        Console.WriteLine($"'{nameSup}' added.");
    }

    public void ReportAll()
    {
        for (int x = 0; x < amount; x++)
        {
            suppliers[x].ReportSupplier();
        }
    }
}
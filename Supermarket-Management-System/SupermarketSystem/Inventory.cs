using System;

    //Inventory class!
    //Calls on ProductStore class
    public class Inventory
    {
    //choice 5 List all products
    private ProductStore[] pocket = new ProductStore[5];
        private int amount = 0;

        public void ReportAll()
        {
            if (amount == 0)
            {
                Console.WriteLine("Currently no products.");
                return;
            }

            for (int x = 0; x < amount; x++)
            {
                pocket[x].ProductReport();
            }
        }

    //Adds a product silently, used when loading from the database on startup
    public void AddLoadedItem(string prodID, string barcode, string prodName, string prodSupplier, string prodCategory, int prodPrice, int prodStock, string prodExpiry)
    {
        if (amount == pocket.Length)
        {
            Array.Resize(ref pocket, pocket.Length * 2);
        }

        var prod = new ProductStore(prodID, barcode, prodName, prodSupplier, prodCategory, prodPrice, prodStock, prodExpiry);
        pocket[amount] = prod;
        amount++;
    }

    //choice 7 -> Low stock report
    public void LowStock()
    {
        Console.WriteLine("-------------------------");
        Console.WriteLine("   Product Stock   ");
        Console.WriteLine("-------------------------");

        //User based low number
        Console.Write("What minimum?: ");
        int.TryParse(Console.ReadLine(), out int min);

        bool found = false;
        for (int x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].prodStock < min)
            {
                pocket[x].ProductReport();
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No low-stock items.");
            Console.WriteLine("-----------------------------------");
        }
    }

    //choice 1 to add new items/products to Inventory
    public void AddItem()
    {

        if (amount == pocket.Length)
        {
            Array.Resize(ref pocket, pocket.Length * 2);
        }

        Console.Write("Barcode?: ");
        string barcode = Console.ReadLine();

        string prodID = Guid.NewGuid().ToString("N").Substring(0, 8);

        Console.Write("Name?: ");
        string prodName = Console.ReadLine();

        Console.Write("Supplier?: ");
        string prodSupplier = Console.ReadLine();

        Console.Write("Category?: ");
        string prodCategory = Console.ReadLine();

        Console.Write("Price?: ");
        string priceInput = Console.ReadLine() ?? "";
        if (!int.TryParse(priceInput, out int prodPrice) || prodPrice <= 0)
        {
            Console.WriteLine("Price must be < 0.");
            return;
        }

        Console.Write("Amount?: ");
        string stockInput = Console.ReadLine() ?? "";
        int prodStock;
        if (!int.TryParse(stockInput, out prodStock) || prodStock < 0)
        {
            Console.WriteLine("Stock < 0.");
            return;
        }
        Console.Write("Expiry Date: ");
        string prodExpiry = Console.ReadLine() ?? "";

        if (amount == pocket.Length)
        {
            Array.Resize(ref pocket, pocket.Length * 2);
        }

        //Checks for doup ID or barcode
        for (int x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].prodID == prodID)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Failed to save");
                Console.WriteLine("");
                Console.WriteLine("Unique ID required.");
                return;
            }
            if (pocket[x] != null && pocket[x].barcode == barcode)
            {
                Console.WriteLine("-------------------------");
                Console.WriteLine("Failed to save!");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Unique Barcode required.");
                return;
            }
        }
        var prod = new ProductStore(prodID, barcode, prodName, prodSupplier, prodCategory, prodPrice, prodStock, prodExpiry);
        pocket[amount] = prod;
        amount++;

        DB.InsertProduct(prodName, barcode, prodSupplier, prodCategory, prodPrice, prodStock, prodExpiry);

        Console.WriteLine($"'{prodName}' was added and saved to database.");

        Console.WriteLine(" ");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine($"'{prodName}' was added.");
        Console.WriteLine(" ");
    }


    //choice 2, removes them from Inventory
    public void RemoveItem()
    {
        Console.WriteLine("Remove with Barcode?>");
        string barcodeChoice = Console.ReadLine() ?? "";

        bool exists = false;
        for (int x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].barcode == barcodeChoice)
            {
                for (int y = x; y < amount - 1; y++)
                {
                    pocket[y] = pocket[y + 1];
                }
                pocket[amount - 1] = null;
                amount--;

                DB.DeleteProduct(barcodeChoice);

                Console.WriteLine(" ");
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Item removed successfully.");
                Console.WriteLine(" ");
                exists = true;
                break;
            }
        }

        if (!exists)
        {
            Console.WriteLine(" ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("No item.");
            Console.WriteLine(" ");
        }
    }


    //choice 3 to update items in Inventory--
    public void UpdateItem()
    {
        Console.WriteLine("Update what item? (Barcode)>");
        string barcodeChoice = Console.ReadLine() ?? "";

        int x;
        for (x = 0; x < amount && pocket[x].barcode != barcodeChoice; x++) { }

        if (x == amount)
        {
            Console.WriteLine("Item not found.");
            return;
        }

        Console.Write("> [1] ");
        Console.WriteLine($"Name: {pocket[x].prodName}");
        Console.Write("> [2] ");
        Console.WriteLine($"Supplier: {pocket[x].prodSupplier}");
        Console.Write("> [3] ");
        Console.WriteLine($"Category: {pocket[x].prodCategory}");
        Console.Write("> [4] ");
        Console.WriteLine($"Price: ${pocket[x].prodPrice}");
        Console.Write("> [5] ");
        Console.WriteLine($"Stock: {pocket[x].prodStock}");
        Console.Write("> [6] ");
        Console.WriteLine($"Expiry Date: {pocket[x].prodExpiration}");
        Console.Write("> [7] ");
        Console.WriteLine($"Barcode: {pocket[x].barcode}");
        Console.WriteLine("-----------");
        Console.WriteLine("> [0] Exit");
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Which value to update?: ");

        string UpdateValue = Console.ReadLine();

        switch (UpdateValue)
        {
            case "1":
                Console.WriteLine("Update Name to:>");
                pocket[x].prodName = Console.ReadLine() ?? "";
                break;

            case "2":
                Console.WriteLine("Update Supplier to:>");
                pocket[x].prodSupplier = Console.ReadLine() ?? "";
                break;

            case "3":
                Console.WriteLine("Update Category to:>");
                pocket[x].prodCategory = Console.ReadLine() ?? "";
                break;

            case "6":
                Console.WriteLine("Update Expiry Date to:>");
                pocket[x].prodExpiration = Console.ReadLine() ?? "";
                break;

            case "7":
                Console.WriteLine("Update Barcode to:>");
                pocket[x].barcode = Console.ReadLine() ?? "";
                break;

            case "4":
                Console.WriteLine("Update Price to:>");
                if (int.TryParse(Console.ReadLine(), out int newPrice) && newPrice > 0)
                {
                    pocket[x].prodPrice = newPrice;
                }
                else
                {
                    Console.WriteLine("Invalid price.");
                }
                break;

            case "5":
                Console.WriteLine("Update Stock to:>");
                if (int.TryParse(Console.ReadLine(), out int newStock) && newStock >= 0)
                {
                    pocket[x].prodStock = newStock;
                }
                else
                {
                    Console.WriteLine("Invalid stock.");
                }
                break;

            case "0":
                Console.WriteLine("Leaving...");
                break;

            default:
                Console.WriteLine("Invalid.");
                break;
        }

        DB.UpdateProduct(barcodeChoice, pocket[x].prodName, pocket[x].prodPrice, pocket[x].prodStock, pocket[x].prodExpiration);
    }


    //case 4 ->search for items in Inventory
    //Search with ID
    public void SearchID()
    {
        Console.Write("Enter Product ID: ");
        string searchID = Console.ReadLine() ?? "";

        int x;
        for (x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].prodID == searchID)
            {
                break;
            }
        }

        if (x == amount)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Product not found.");
            Console.WriteLine("");
            return;
        }

        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Product Found:");
        pocket[x].ProductReport();
        Console.WriteLine("-");
    }

    //Search with Name 
    public void SearchName()
    {
        Console.Write("Enter Name: ");
        string searchName = Console.ReadLine() ?? "";

        int x;
        for (x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].prodName == searchName)
            {
                break;
            }
        }

        if (x == amount)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Product not found.");
            Console.WriteLine("");
            return;
        }

        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Product Found:");
        pocket[x].ProductReport();
        Console.WriteLine("");
    }

    //Search with Barcode
    public void SearchBarcode()
    {
        Console.Write("Enter Barcode: ");
        string searchBarcode = Console.ReadLine() ?? "";

        ProductStore[] searcher = new ProductStore[amount];
        for (int x = 0; x < amount; x++)
        {
            searcher[x] = pocket[x];
        }
        Array.Sort(searcher, (y, z) => string.Compare(y.barcode, z.barcode));

        //Binary search for second search method
        int low = 0, high = searcher.Length - 1;
        int foundBar = -1;

        while (low <= high)
        {
            int min = (low + high) / 2;
            int compare = string.Compare(searcher[min].barcode, searchBarcode);

            if (compare == 0)
            {
                foundBar = min;
                break;
            }
            else if (compare < 0)
            {
                low = min + 1;
            }
            else
            {
                high = min - 1;
            }
        }

        if (foundBar == -1)
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Product not found.");
            Console.WriteLine("");
            return;
        }

        Console.WriteLine("-----------------------------------");
        Console.WriteLine("Product Found:");
        searcher[foundBar].ProductReport();
        Console.WriteLine("");
    }

    //two search methods for items in Inventory
    public void SearchItem()
    {
        Console.WriteLine("-----------------------------------");
        Console.WriteLine("> [1] Search ID?: ");
        Console.WriteLine("> [2] Search Barcode?: ");
        Console.WriteLine("> [3] Search Name?: ");
        Console.WriteLine("-----------");
        Console.WriteLine("> [0] Leave?>");
        Console.WriteLine("-----------------------------------");
        string SearchChoice = Console.ReadLine();

        if (SearchChoice == "1")
        {
            SearchID();
        }
        else if (SearchChoice == "3")
        {
            SearchName();
        }
        else if (SearchChoice == "2")
        {
            SearchBarcode();
        }
        else if (SearchChoice == "0")
        {
            Console.WriteLine("Leaving...");
            return;
        }
        else
        {
            Console.WriteLine("Invalid choice.");
            return;
        }
    }

    //Lowers stock for sold product
    public bool LowerStock(string prodID, int amtSold)
    {
        for (int x = 0; x < amount; x++)
        {
            if (pocket[x] != null && pocket[x].prodID == prodID)
            {
                if (pocket[x].prodStock < amtSold)
                {
                    Console.WriteLine($"Can't sell ' {pocket[x].prodName}'. More than stock: {pocket[x].prodStock}");
                    return false;
                }

                pocket[x].prodStock -= amtSold;
                Console.WriteLine($"Stock change: '{pocket[x].prodName}' has {pocket[x].prodStock} left.");
                return true;
            }
        }
        Console.WriteLine("Product ID is not there.");
        return false;
    }


}

using System;
using Microsoft.Data.SqlClient;

class Program
{

    static void Main(string[] args)
    {
        bool startMenu = true;
        Inventory inven = new Inventory();

        while (startMenu)
        {
            //Print the menu for user to choose from
            Console.WriteLine();
            Console.WriteLine("====================================");
            Console.WriteLine("   SUPERMARKET MANAGEMENT SYSTEM    ");
            Console.WriteLine("====================================");
            Console.WriteLine("1. Add product");
            Console.WriteLine("2. Remove product");
            Console.WriteLine("3. Update product");
            Console.WriteLine("4. Search");
            Console.WriteLine("5. List All prods");
            Console.WriteLine("6. Exit");
            Console.WriteLine("-----------------------------------");
            Console.Write(">Choose from options above: ");
            //Read user input
            string option = Console.ReadLine() ?? "";

            //respond based on user input
            switch (option)
            {
                case "1":
                    inven.AddItem();
                    break;
                case "2":
                    inven.RemoveItem();
                    break;
                case "3":
                    Console.WriteLine("Update what?" + " TBA");
                    break;
                case "4":
                    Console.WriteLine("Search:" + " TBA");
                    break;
                case "5":
                    inven.ReportAll();
                    break;
                case "6":
                    startMenu = false;
                    Console.WriteLine("Closing");
                    break;
                default:
                    Console.WriteLine("Not Valid choice.");
                    break;
            }
        }
    }



//-------------------------------------------------------------------------------

    //Calls on ProductStore class
    public class Inventory
    {
        private ProductStore[] Inven = new ProductStore[5];
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
                Inven[x].Report();
            }
        }

//-------------------------------------------------------------------


        //Case 1 to add new items/products to inventory
        public void AddItem()
        {
            Console.Write("Give ID: ");
            string itemID = Console.ReadLine() ?? "";

            Console.Write("Name?: ");
            string prodName = Console.ReadLine() ?? "";

            Console.Write("Brand?: ");
            string prodBrand = Console.ReadLine() ?? "";

            Console.Write("Category?: ");
            string prodCategory = Console.ReadLine() ?? "";

            Console.Write("Price?: ");
            string priceInput = Console.ReadLine() ?? "";
            int prodPrice;
            if (!int.TryParse(priceInput, out prodPrice))
            {
                Console.WriteLine("Invalid price input. Using 0.");
                prodPrice = 0;
            }

            Console.Write("Amount?: ");
            string stockInput = Console.ReadLine() ?? "";
            int prodStock;
            if (!int.TryParse(stockInput, out prodStock))
            {
                Console.WriteLine("Invalid amount input. Using 0.");
                prodStock = 0;
            }

            Console.Write("Expiry Date: ");
            string prodExpiry = Console.ReadLine() ?? "";

            if (amount == Inven.Length)
            {
                Array.Resize(ref Inven, Inven.Length * 2);
            }

            var prod = new ProductStore(itemID, prodName, prodBrand, prodCategory, prodPrice, prodStock, prodExpiry);

            Inven[amount] = prod;
            amount++;

            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"'{prodName}' was added.");
            Console.WriteLine("-----------------------------------");

        }



//-------------------------------------------------------------------

        //case 2 to remove them from inventory
        public void RemoveItem()
        {
            Console.WriteLine("Remove with ID?>");
            string IDchoice = Console.ReadLine() ?? "";

            bool exists = false;

            for (int x = 0; x < amount; x++)
            {
                if (Inven[x] != null && Inven[x].prodID == IDchoice)
                {
                    for (int y = x; y < amount - 1; y++)
                    {
                        Inven[y] = Inven[y + 1];
                    }

                    Inven[amount - 1] = null;

                    amount--;

                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Item removed successfully.");
                    Console.WriteLine("-----------------------------------");
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("No item.");
                Console.WriteLine("-----------------------------------");
            }
        }



//-------------------------------------------------------------------

        //case 3 to update items in inventory
        public void UpdateItem()
        {
            Console.WriteLine("Update what item?>");
            string UDchoice = Console.ReadLine() ?? "";

            // implement update logic...
        }
    }
}

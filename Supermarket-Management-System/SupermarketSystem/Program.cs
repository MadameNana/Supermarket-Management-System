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
            Console.WriteLine("> [1] Add product");
            Console.WriteLine("> [2] Remove product");
            Console.WriteLine("> [3] Update product");
            Console.WriteLine("> [4] Search");
            Console.WriteLine("> [5] List All prods");
            Console.WriteLine("-----------");
            Console.WriteLine("> [0] Exit");
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
                    inven.UpdateItem();
                    break;
                case "4":
                    inven.SearchItem();
                    break;
                case "5":
                    inven.ReportAll();
                    break;
                case "0":
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
                Inven[x].ProductReport();
            }
        }


//-------------------------------------------------------------------

        //--case 1 to add new items/products to inventory--
        public void AddItem()
        {
            Console.Write("Give ID: ");
            string itemID = Console.ReadLine() ?? "";

            Console.Write("Name?: ");
            string prodName = Console.ReadLine() ?? "";

            Console.Write("Supplier?: ");
            string prodSupplier = Console.ReadLine() ?? "";

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

            var prod = new ProductStore(itemID, prodName, prodSupplier, prodCategory, prodPrice, prodStock, prodExpiry);

            Inven[amount] = prod;
            amount++;

            Console.WriteLine(" ");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"'{prodName}' was added.");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine(" ");
        }


//-------------------------------------------------------------------

        //--case 2 to remove them from inventory--
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

                    Console.WriteLine(" ");
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Item removed successfully.");
                    Console.WriteLine("-----------------------------------");
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
                Console.WriteLine("-----------------------------------");
                Console.WriteLine(" ");
            }
        }


//-------------------------------------------------------------------

        //--case 3 to update items in inventory--
        public void UpdateItem()
        {
            Console.WriteLine("Update what item?>");
            string UpdateID = Console.ReadLine() ?? "";

            int x;
            for (x = 0; x < amount && Inven[x].prodID != UpdateID; x++) { }

            if (x == amount)
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Item not found.");
                Console.WriteLine("-----------------------------------");
                return;
            }


            //Update item details Menu
            Console.WriteLine();
            Console.WriteLine("==========================");
            Console.WriteLine("   Manage Product details    ");
            Console.WriteLine("==========================");
            Console.Write("> [1] ");
            Console.WriteLine($"Name: {Inven[x].prodName}");
            Console.Write("> [2] ");
            Console.WriteLine($"ID: {Inven[x].prodID}");
            Console.Write("> [3] ");
            Console.WriteLine($"Supplier: {Inven[x].prodSupplier}");
            Console.Write("> [4] ");
            Console.WriteLine($"Category: {Inven[x].prodCategory}");
            Console.Write("> [5] ");
            Console.WriteLine($"Price: ${Inven[x].prodPrice}");
            Console.Write("> [6] ");
            Console.WriteLine($"Stock: {Inven[x].prodStock}");
            Console.Write("> [7] ");
            Console.WriteLine($"Expiry Date: {Inven[x].prodExpiration}");
            Console.WriteLine("-----------");
            Console.WriteLine("> [0] Exit");
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Which value to update?: ");

            string UpdateValue = Console.ReadLine();

            //User choice for value to update
            switch (UpdateValue)
            {
                case "1":
            Console.WriteLine("Update Name to:>");
                    Inven[x].prodName = Console.ReadLine() ?? "";
                    break;

                case "2":
            Console.WriteLine("Update ID to:>"); 
                    Inven[x].prodID = Console.ReadLine() ?? "";
                    break;

                case "3":
            Console.WriteLine("Update Supplier to:>");
                    Inven[x].prodSupplier = Console.ReadLine() ?? "";
                    break;

                case "4":
            Console.WriteLine("Update Category to:>");
                    Inven[x].prodCategory = Console.ReadLine() ?? "";
                    break;

                case "5":
            Console.WriteLine("Update Price to:>");
                    if (int.TryParse(Console.ReadLine(), out int newPrice))
                    {
                        Inven[x].prodPrice = newPrice;
                    }
                    else
                    {
                        Console.WriteLine("Invalid price.");
                    }
                    break;

                case "6":
            Console.WriteLine("Update Stock to:>");
                    if (int.TryParse(Console.ReadLine(), out int newStock))
                    {
                        Inven[x].prodStock = newStock;
                    }
                    else
                    {
                        Console.WriteLine("Invalid stock.");
                    }
                    break;

                case "7":
            Console.WriteLine("Update Expiry Date to:>");
                    Inven[x].prodExpiration = Console.ReadLine() ?? "";
                    break;

                case "0":
                    Console.WriteLine("Leaving...");
                    break;

                default:
                    Console.WriteLine("Invalid.");
                    break;
            }
        }


//-------------------------------------------------------------------

        //case 4 search for items in inventory
        //Search with ID
        public void SearchID()
        {
            Console.Write("Enter Product ID: ");
            string searchID = Console.ReadLine() ?? "";

            int x;
            for (x = 0; x < amount; x++)
            {
                if (Inven[x] != null && Inven[x].prodID == searchID)
                {
                    break;
                }
            }

            if (x == amount) 
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Product not found.");
                Console.WriteLine("-----------------------------------");
                return;
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Product Found:");
            Inven[x].ProductReport();
            Console.WriteLine("-----------------------------------");
        }

        //Search with Name
        public void SearchName()
        {
            Console.Write("Enter Name: ");
            string searchName = Console.ReadLine() ?? "";

            int x;
            for (x = 0; x < amount; x++)
            {
                if (Inven[x] != null && Inven[x].prodName == searchName)
                {
                    break;
                }
            }

            if (x == amount) 
            {
                Console.WriteLine("-----------------------------------");
                Console.WriteLine("Product not found.");
                Console.WriteLine("-----------------------------------");
                return;
            }

            Console.WriteLine("-----------------------------------");
            Console.WriteLine("Product Found:");
            Inven[x].ProductReport();
            Console.WriteLine("-----------------------------------");
        }

        //two search methods for items in inventory
        public void SearchItem()
        {
            Console.WriteLine("-----------------------------------");
            Console.WriteLine("> [1] Search ID?: ");
            Console.WriteLine("> [2] Search Name?: ");
            Console.WriteLine("-----------");
            Console.WriteLine("> [0] Leave?>");
            Console.WriteLine("-----------------------------------");
            string SearchChoice = Console.ReadLine();

            if (SearchChoice == "1")
            {
                SearchID();
            }
            else if (SearchChoice == "2")
            {
                SearchName();
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
    }
}

using System;
using Microsoft.Data.SqlClient;

class Program
{

    static void Main(string[] args)
    {
        bool startMenu = true;
        inven inven = new inven();

        while (startMenu)
        {
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

            string option = Console.ReadLine();


            switch (option)
            {
                case "1":
                    inven.AddItem();
                    break;
                case "2":
                    Console.WriteLine("Remove what?" + " TBA");
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

    public class inven
    {
        private ProductStore[] inven = new ProductStore[5];
        private int amount = 0;

        public void ReportAll()
        {
            if (amount == 0)
            {
                Console.WriteLine("No products in inven.");
                return;
            }

            for (int x = 0; x < amount; x++)
            {
                inven[x].Report();
            }
        }
        public void AddItem()
        {
            Console.Write("Give ID: ");
            string prodID = Console.ReadLine();

            Console.Write("What Name?: ");
            string prodName = Console.ReadLine();

            Console.Write("What Brand?: ");
            string prodBrand = Console.ReadLine();

            Console.Write("What Category?: ");
            string prodCategory = Console.ReadLine();

            Console.Write("What Price?: ");
            int prodPrice = Convert.ToInt32(Console.ReadLine());

            Console.Write("What Amount?: ");
            int prodStock = Convert.ToInt32(Console.ReadLine());

            Console.Write("Expiry Date: ");
            string prodExpiry = Console.ReadLine();

            if (amount == inven.Length)
            {
                Array.Resize(ref inven, inven.Length * 2);
            }


            var prod = new ProductStore(prodID, prodName, prodBrand, prodCategory, prodPrice, prodStock, prodExpiry);

            inven[amount] = prod;
            amount++;


            Console.WriteLine("-----------------------------------");
            Console.WriteLine($"'{prodName}' was added.");
            Console.WriteLine("-----------------------------------");

        }


    }
}

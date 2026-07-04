using System;
using Microsoft.Data.SqlClient;


class Program
{
    //Every Console need a "Main" method to run a program.
    static void Main(string[] args)
    {
        //DB.TestConnection();
        //Starts the main menu which is a while loop, it will keep looping until the user presses 0 to exit and stop it.
        bool startMenu = true;
        //An object of Inventory class to manage in other classes.
        Inventory pocket = new Inventory();
        DB.LoadProducts(pocket);
        //An object of ManagerChoices class to help manage the management menu
        ManageChoices mng = new ManageChoices(pocket);

        while (startMenu)
        {
            //It is just a lot of printing new lines to create a more uniform look.
            Console.WriteLine();
            Console.WriteLine("=====================================");
            Console.WriteLine("SuperMarket System Menu");
            Console.WriteLine("=====================================");
            Console.WriteLine("> [1] Add product");
            Console.WriteLine("> [2] Remove product");
            Console.WriteLine("> [3] Update product");
            Console.WriteLine("> [4] Search");
            Console.WriteLine("> [5] List All prods");
            Console.WriteLine("> [6] Management Settings");
            Console.WriteLine("> [7] Low stock");
            Console.WriteLine("-----------");
            Console.WriteLine("> [0] Exit");
            Console.WriteLine("-----------------------------------");
            Console.Write(">Choose from options above: ");

            string option = Console.ReadLine() ?? "";

            switch (option)
            { //This class on AddItem, so the person typing can add items into this.
                case "1":
                    pocket.AddItem();
                    break;
                //This class on removeItem, this does the opposite of the above choice, it removes items based on their Id.
                case "2":
                    pocket.RemoveItem();
                    break;
                //This class on UpdateItem, this one lets you change anything about the item: Name, price, category etc.
                case "3":
                    pocket.UpdateItem();
                    break;
                //This class on SearchItem, this calls on the search fuction, it lets you search based on name, id or barcode.
                case "4":
                    pocket.SearchItem();
                    break;
                case "5":
                    //This class on ReportAll, this one shows you all the items currently in the inventory with their details.
                    pocket.ReportAll();
                    break;
                case "6":
                    //This class on ManagementMenu, this allows you to manage internal stuff, like the sales, suppliers and categories.
                    mng.ManagementMenu();
                    break;
                //This class on LowStock, asks you for the number you assume is low enough stock and shows you all the items below it.
                case "7":
                    pocket.LowStock();
                    break;
                //this closes it, because having an infinite unending loop isn't efficient.
                case "0":
                    startMenu = false;
                    Console.WriteLine("Closing");
                    break;
                //If you accidentally type the wrong number.
                default:
                    Console.WriteLine("Not Valid choice.");
                    break;
            }
        }
    }

    public class ManageChoices
    {
        //there's three of them: sales, categories and supppliers, they're private variables, they're only used in here.
        //Sales manager is for adding sales information about products.
        private SalesManager salesManage = new SalesManager();
        //Category manager, lets you add add categories for products to be categorised as.
        private CategoryManager cateManage = new CategoryManager();
        //and lastly the supplier manager, lets you add supplier who provide said products.
        private SupplierManager supManage = new SupplierManager();
        //Creates the management using the existing Inventory.
        private Inventory pocket;

        public ManageChoices(Inventory pocket)
        {
            this.pocket = pocket;
        }

        public void ManagementMenu()
        {
            //Here's the Manager menu, like the one above, prints out a bunch of stuff, visual cues for the person on the console.
            Console.WriteLine();
            Console.WriteLine("============================");
            Console.WriteLine("   Management Settings   ");
            Console.WriteLine("============================");
            Console.WriteLine("> [1] Manage Sales");
            Console.WriteLine("> [2] Manage Categories");
            Console.WriteLine("> [3] Manage Suppliers");
            Console.WriteLine("-----------");
            Console.WriteLine("> [0] Exit");
            Console.WriteLine("-----------------------------");
            Console.Write(">Choose from options above: ");
            string manageOption = Console.ReadLine() ?? "";

            switch (manageOption)
            {
                //The options are pretty self explanatory, you choose what you want to do and do it based one the following choice you get from the manager class.
                case "1":
                    salesManage.AddSale(pocket);
                    salesManage.ReportAll();
                    break;
                case "3":
                    supManage.AddSup();
                    supManage.ReportAll();
                    break;
                case "2":
                    cateManage.AddCategory();
                    cateManage.ReportAll();
                    break;
                case "0":
                    Console.WriteLine("Closing");
                    break;
                default:
                    Console.WriteLine("Not Valid choice.");
                    break;
            }
        }
    }
}
//I apologise for the excessive anatating, I thought thoughrough exaplanation was necessary and got ahead of myself

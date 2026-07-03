using System;

//Storing Categories class for the supermarket system
public class CategoryStore
{
    public string cateID { get; set; }
    public string cateName { get; set; }

    public CategoryStore(string cateID, string cateName)
    {
        this.cateID = cateID;
        this.cateName = cateName;
    }


    //-------------------------------
    //--Report Method
    public void ReportCategories()
    {
        Console.WriteLine("Categories:");
        Console.WriteLine($" ID: {cateID}  \n  Name: '{cateName}' \n --------------------");
        Console.WriteLine(" ");
    }

}
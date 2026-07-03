using System;

//Supplier stored in the supermarket system
public class SupplierStore
{
    public string supID { get; set; }
    public string supName { get; set; }
    public string supContact { get; set; }

    public SupplierStore(string supID, string supName, string supContact)
    {
        this.supID = supID;
        this.supName = supName;
        this.supContact = supContact;
    }


//-------------------------------
//--Report Method
    public void ReportSupplier()
    {
        Console.WriteLine("Supplier:");
        Console.WriteLine($" Supplier ID: {supID}  \n Supplier Name: '{supName}' \n Supplier Contact: '{supContact}' \n --------------------");
        Console.WriteLine(" ");
    }

}
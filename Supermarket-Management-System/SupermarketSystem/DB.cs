using System;
using Microsoft.Data.SqlClient;

public class DB
{
    private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=SuperMarketDB;Trusted_Connection=True;TrustServerCertificate=True;";

    //--------------------------------------------------------------------------------------------------------------------------------------
    //Test the connection
    public static void TestConnection()
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                Console.WriteLine("Connected to database successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Connection failed: " + ex.Message);
        }
    }

    //--------------------------------------------------------------------------------------------------------------------------------------
    //Insert a new product into Items
    public static void InsertProduct(string name, string barcode, string supplierName, string categoryName, int price, int stock, string expiry)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            //Look up or insert supplier
            int supplierID = GetOrCreateSupplier(conn, supplierName);
            int categoryID = GetOrCreateCategory(conn, categoryName);

            string query = @"INSERT INTO Items (ProductName, Barcode, SupplierID, CategoryID, Price, Stock, ExpirationDate)
                          VALUES (@Name, @Barcode, @Supplier, @Category, @Price, @Stock, @Expiry)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Barcode", barcode);
                cmd.Parameters.AddWithValue("@Supplier", supplierID);
                cmd.Parameters.AddWithValue("@Category", categoryID);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Expiry", expiry);  
                cmd.ExecuteNonQuery();
            }
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------
    private static int GetOrCreateSupplier(SqlConnection conn, string name)
    {
        string checkQuery = "SELECT SupplierID FROM Supplier WHERE SupplierName = @Name";
        using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            object result = cmd.ExecuteScalar();
            if (result != null) return (int)result;
        }

        string insertQuery = "INSERT INTO Supplier (SupplierName) OUTPUT INSERTED.SupplierID VALUES (@Name)";
        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            return (int)cmd.ExecuteScalar();
        }
    }

    private static int GetOrCreateCategory(SqlConnection conn, string name)
    {
        string checkQuery = "SELECT CategoryID FROM Category WHERE CategoryName = @Name";
        using (SqlCommand cmd = new SqlCommand(checkQuery, conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            object result = cmd.ExecuteScalar();
            if (result != null) return (int)result;
        }

        string insertQuery = "INSERT INTO Category (CategoryName) OUTPUT INSERTED.CategoryID VALUES (@Name)";
        using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
        {
            cmd.Parameters.AddWithValue("@Name", name);
            return (int)cmd.ExecuteScalar();
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------
    //List all products
    public static void ListProducts()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = "SELECT ProductID, ProductName, Barcode, Price, Stock FROM Items";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["ProductID"]} | {reader["ProductName"]} | {reader["Barcode"]} | £{reader["Price"]} | Stock: {reader["Stock"]}");
                }
            }
        }
    }


    //List categories so the user can pick one
    public static void ListCategories()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT CategoryID, CategoryName FROM Category";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"[{reader["CategoryID"]}] {reader["CategoryName"]}");
                }
            }
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------
    //List suppliers so the user can pick one
    public static void ListSuppliers()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT SupplierID, SupplierName FROM Supplier";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Console.WriteLine($"[{reader["SupplierID"]}] {reader["SupplierName"]}");
                }
            }
        }
    }


    //--------------------------------------------------------------------------------------------------------------------------------------
    //Load all products from the database into the given Inventory
    public static void LoadProducts(Inventory inven)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = @"SELECT i.ProductID, i.ProductName, i.Barcode, s.SupplierName, c.CategoryName,
                                     i.Price, i.Stock, i.ExpirationDate
                              FROM Items i
                              LEFT JOIN Supplier s ON i.SupplierID = s.SupplierID
                              LEFT JOIN Category c ON i.CategoryID = c.CategoryID";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    string prodID = reader["ProductID"].ToString();
                    string barcode = reader["Barcode"].ToString();
                    string name = reader["ProductName"].ToString();
                    string supplier = reader["SupplierName"].ToString();
                    string category = reader["CategoryName"].ToString();
                    int price = Convert.ToInt32(reader["Price"]);
                    int stock = Convert.ToInt32(reader["Stock"]);
                    string expiry = Convert.ToDateTime(reader["ExpirationDate"]).ToString("yyyy-MM-dd");

                    inven.AddLoadedItem(prodID, barcode, name, supplier, category, price, stock, expiry);
                }
            }
        }

        Console.WriteLine("Products loaded from database.");
    }

    //Delete a product by barcode
    public static void DeleteProduct(string barcode)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = "DELETE FROM Items WHERE Barcode = @Barcode";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Barcode", barcode);
                cmd.ExecuteNonQuery();
            }
        }
    }

    //Update a product's details by barcode
    public static void UpdateProduct(string barcode, string name, int price, int stock, string expiry)
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();

            string query = @"UPDATE Items 
                              SET ProductName = @Name, Price = @Price, Stock = @Stock, ExpirationDate = @Expiry
                              WHERE Barcode = @Barcode";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Price", price);
                cmd.Parameters.AddWithValue("@Stock", stock);
                cmd.Parameters.AddWithValue("@Expiry", expiry);
                cmd.Parameters.AddWithValue("@Barcode", barcode);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
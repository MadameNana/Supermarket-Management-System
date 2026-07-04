Local Supermarket Management System for Small Shops
A C# .NET console application for managing day-to-day logging for products, suppliers, categories, stock, and sales, with a SQL Server database.

Features
•	Add, update, remove, and search products (by ID, name, and barcode).
•	Two different search algorithms: linear search (name) and binary search (barcode).
•	Management menu for suppliers and categories.
•	Low-stock report with a user decided minimum stock.
•	Data persistence to SQL Server (products, suppliers, categories)
•	Input validation (unique product IDs/barcodes, positive prices, non-negative stock)

Requires
•	.NET SDK (6.0 or later)
•	SQL Server Management Studio (SSMS) or Visual Studio's SQL Server Object Explorer, for running the schema script

Database Setup
1.	Open SSMS (or Visual Studio → Tools > Connect to Database > Microsoft SQL Server) and connect to your SQL Server.
2.	To create a database, you can use a query window and run database/schema.sql (can be done in this repo). This will: 
o	Create the SuperMarketDB database
o	Create the Category, Supplier, Items tables with the correct relationships and constraints
o	Insert a small set of sample categories and suppliers for testing
3.	Confirm the database and tables were created by expanding SuperMarketDB in Object Explorer.

Configurations
Before running the application, you need to point it at your own SQL Server:
1.	Open DB.cs.
2.	Find this line near the top of the file : 
3.	private static string connectionString = "Server=locahost\\SQLEXPRESS;Database=SuperMarketDB;Trusted_Connection=True;TrustServerCertificate=True;" 
4.	Note: ensure you create a “localhost” SQL server, name can be changed if deemed fitting.

Running
From the project folder (where the .csproj file is located) type in the terminal:
dotnet build
dotnet run
On startup, the application automatically loads existing products from the database into memory, you can use that added data.

Using
The application is menu-based. From the main menu you can select:

Options	
1	Add a new product
2	Remove a product (by barcode)
3	Update a product's details (by barcode)
4	Search for a product (by ID, name, or barcode)
5	List all products
6	Open Management Settings (Sales, Categories, Suppliers)
7	View a low-stock report
0	Exit
Within Management Settings (6), you can calculate a sale (which automatically the amount of reduced stock for each sale entry), add a product category, or add a supplier.

Project Structure
Program.cs            Main menu and application entry point
Inventory.cs           Product management: add, remove, update, search, low-stock report
ProductStore.cs         Product entity
SupplierStore.cs        Supplier entity
CategoryStore.cs        Category entity
SalesTrack.cs           Represents a single sale and its line items 
SoldStore.cs            A single sold item within a sale 
Manager.cs             SalesManager, CategoryManager, SupplierManager classes
DB.cs                  All SQL Server data access (insert, update, delete, load)
database/schema.sql   SQL script to create the database schema and seed data

Limitations
•	The database's auto-generated ProductID and product IDs are separate from barcode, which is used as a practical unique key for update/delete products in the database.
•	Stock changes are not currently logged, can assist with calculations for manual change, with Update option.
•	The system was made for a single user at a time.

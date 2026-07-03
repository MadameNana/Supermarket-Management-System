using Microsoft.EntityFrameworkCore;

public class DB : DbContext
{
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost;Database=SuperMarketDB;Trusted_Connection=True;");
    }
}
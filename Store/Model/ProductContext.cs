using Microsoft.EntityFrameworkCore;

namespace Store.Model;

public class ProductContext : DbContext
{
    private readonly string _connectionString;
    public DbSet<Product> Products { get; set; }

    public ProductContext(string connectionString)
    {
        _connectionString = connectionString;
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_connectionString);
    }
}
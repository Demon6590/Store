using System.Collections.Generic;
using System.Linq;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.isDelete)
            .HasDefaultValue(false);
    }

    public void AddProduct(Product p)
    {
        Products.Add(p);
        SaveChanges();
    }

    public void SoftDelete(string article)
    {
        var product = Products.Find(article);
        var updated = product with { isDelete = true };
        Entry(product).CurrentValues.SetValues(updated);
        SaveChanges();
    }

    public void UpdateProduct(Product p)
    {
        var existing = Products.Find(p.Article);
        Entry(existing).CurrentValues.SetValues(p);
        SaveChanges();
    }

    public IEnumerable<Product> GetAll()
    {
        return Products
            .Where(p => !p.isDelete)
            .ToList();
    }

    public IEnumerable<Product> GetByName(string name)
    {
        return Products
            .Where(p => p.Name == name && !p.isDelete)
            .ToList();
    }


    public IEnumerable<Product> GetByPrice(decimal price)
    {
        return Products
            .Where(p => p.Price == price && !p.isDelete)
            .ToList();
    }

    public IEnumerable<Product> GetByStock(int stock)
    {
        return Products
            .Where(p => p.StockQuantity == stock && !p.isDelete)
            .ToList();
    }
}
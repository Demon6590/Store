using System.Collections.Generic;
using Microsoft.Data.Sqlite;
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
    
    public bool AddProduct(Product p)
    {
        const string sql = @"
                           INSERT INTO table_products (article, name, manufacturer, price, stock_quantity) 
                           VALUES (@Article, @Name, @Manufacturer, @Price, @StockQuantity)
                           ";
        var parameters = new[]
        {
            new SqliteParameter("@Article", p.Article),
            new SqliteParameter("@Name", p.Name),
            new SqliteParameter("@Manufacturer", p.Manufacturer),
            new SqliteParameter("@Price", p.Price),
            new SqliteParameter("@StockQuantity", p.StockQuantity),
        };
        var result = Database.ExecuteSqlRaw(sql,parameters);
        
        return result > 0;
        
    }
    public bool SoftDelete(string article)
    {
        const string sql = "UPDATE table_products SET is_delete = 1 WHERE article = @Article";
        var result = Database.ExecuteSqlRaw(sql, new SqliteParameter("@Article", article));
        return result > 0;
    }
    
    public bool UpdateProduct(Product p)
    {
        const string sql = @"
            UPDATE table_products 
            SET name = @Name, manufacturer = @Manufacturer, price = @Price, stock_quantity = @Stock 
            WHERE article = @Article
            ";

        var parameters = new[] {
            new SqliteParameter("@Name", p.Name),
            new SqliteParameter("@Manufacturer", p.Manufacturer),
            new SqliteParameter("@Price", p.Price),
            new SqliteParameter("@Stock", p.StockQuantity),
            new SqliteParameter("@Article", p.Article)
        };

        var result = Database.ExecuteSqlRaw(sql, parameters);
        
        return result > 0;
    }

    public IEnumerable<Product> GetAll()
    {
        const string sql = "SELECT * FROM table_products WHERE is_delete = 0";
        return Products.FromSqlRaw(sql);
    }

    public IEnumerable<Product> GetByName(string name)
    {
        const string sql = "SELECT * FROM table_products WHERE name = @Name AND is_delete = 0";

        var parameter = new SqliteParameter("@Name", name);
        return Products.FromSqlRaw(sql, parameter);
    }

    
    public IEnumerable<Product> GetByPrice(decimal price)
    {
        const string sql = "SELECT * FROM table_products WHERE price = @Price AND is_delete = 0";
        var parameter = new SqliteParameter("@Price", price);
    
        return Products.FromSqlRaw(sql, parameter);
    }
    
    public IEnumerable<Product> GetByStock(int stock)
    {
        const string sql = "SELECT * FROM table_products WHERE stock_quantity = @Stock AND is_delete = 0";
        var parameter = new SqliteParameter("@Stock", stock);
    
        return Products.FromSqlRaw(sql, parameter);
    }
}
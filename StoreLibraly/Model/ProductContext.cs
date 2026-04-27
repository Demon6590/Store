using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Store.Model;

/// <summary>
/// Класс работающий с бд на SQLite
/// </summary>
public class ProductContext : DbContext
{
    private readonly string _connectionString;
    /// <summary>
    /// Набор данных продуктов в базе.
    /// </summary>
    public DbSet<Product> Products { get; set; }
    /// <summary>
    /// Инициализирует новый экземпляр <see cref="ProductContext"/>.
    /// </summary>
    /// <param name="connectionString">Строка подключения к базе данных SQLite.</param>
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
    /// <summary>
    /// Добавляет новый продукт в базу данных.
    /// </summary>
    /// <param name="p">Экземпляр продукта для добавления.</param>
    public void AddProduct(Product p)
    {
        Products.Add(p);
        SaveChanges();
    }
    /// <summary>
    /// Выполняет "мягкое удаление" продукта (устанавливает флаг isDelete в true).
    /// </summary>
    /// <param name="article">Артикул продукта, который необходимо пометить как удаленный.</param>
    public void SoftDelete(string article)
    {
        var product = Products.Find(article);
        var updated = product with { isDelete = true };
        Entry(product).CurrentValues.SetValues(updated);
        SaveChanges();
    }
    /// <summary>
    /// Обновляет данные существующего продукта.
    /// </summary>
    /// <param name="p">Продукт с новыми данными (поиск ведется по артикулу).</param>
    public void UpdateProduct(Product p)
    {
        var existing = Products.Find(p.Article);
        Entry(existing).CurrentValues.SetValues(p);
        SaveChanges();
    }
    /// <summary>
    /// Возвращает все активные (неудаленные) продукты.
    /// </summary>
    /// <returns>Коллекция продуктов.</returns>
    public IEnumerable<Product> GetAll()
    {
        return Products
            .Where(p => !p.isDelete)
            .ToList();
    }
    /// <summary>
    /// Поиск продуктов по имени.
    /// </summary>
    /// <param name="name">Имя продукта для поиска.</param>
    /// <returns>Список продуктов с указанным именем.</returns>
    public IEnumerable<Product> GetByName(string name)
    {
        return Products
            .Where(p => p.Name == name && !p.isDelete)
            .ToList();
    }

    /// <summary>
    /// Поиск продуктов по цене.
    /// </summary>
    /// <param name="price">Цена для поиска.</param>
    /// <returns>Список продуктов с указанной ценой.</returns>
    public IEnumerable<Product> GetByPrice(decimal price)
    {
        return Products
            .Where(p => p.Price == price && !p.isDelete)
            .ToList();
    }
    /// <summary>
    /// Поиск продуктов по количеству на складе.
    /// </summary>
    /// <param name="stock">Количество для поиска.</param>
    /// <returns>Список продуктов с указанным остатком.</returns>
    public IEnumerable<Product> GetByStock(int stock)
    {
        return Products
            .Where(p => p.StockQuantity == stock && !p.isDelete)
            .ToList();
    }
}
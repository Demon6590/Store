using System.Linq;
using Store.Model;

namespace TestStore;

public class UnitTest1
{
    
    private ProductContext CreateContext()
    {
        var context = new ProductContext(@"Data Source=C:\Users\college\RiderProjects\Store\TestStore\test.db;");
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }
    [Fact]
    public void GetAllTest()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "A", "M", 10, 1, false));
        context.AddProduct(new Product("2", "B", "M", 20, 1, true));
        var result = context.GetAll();
        
        Assert.Single(result);
    }
    [Fact]
    public void GetByNameTest()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "Laptop", "Brand", 1000, 5, false));
        context.AddProduct(new Product("2", "Phone", "Brand", 500, 10, false));

        var result = context.GetByName("Laptop").ToList();
        
        Assert.Equal("Laptop", result[0].Name);
    }
    
    [Fact]
    public void GetByPriceTest()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "P1", "M", 100, 1, false));
        context.AddProduct(new Product("2", "P2", "M", 100, 2, false));
        context.AddProduct(new Product("3", "P3", "M", 200, 1, false));

        var result = context.GetByPrice(100).ToList();

        Assert.Equal(2, result.Count);
    }
    [Fact]
    public void GetByStockTest()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "P1", "M", 10, 50, false));
        context.AddProduct(new Product("2", "P2", "M", 10, 20, false));

        var result = context.GetByStock(50).ToList();

        Assert.Single(result);
        Assert.Equal(50, result[0].StockQuantity);
    }
    [Fact]
    public void GetByNameTestNegative()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "Existing", "M", 10, 1, false));
        
        var result = context.GetByName("NonExistent");

        Assert.Empty(result);
    }
    
    [Fact]
    public void GetByPriceTestNegative()
    {
        var context = CreateContext();
        context.AddProduct(new Product("1", "P1", "M", 100, 1, false));
        
        var result = context.GetByPrice(999);

        Assert.Empty(result);
    }
    
    
}
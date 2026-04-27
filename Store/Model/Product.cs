using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Model;

[Table("table_products")]
public record Product(
    [property: Key]
    [property: Column("article")] string Article,
    [property: Column("name")] string Name,
    [property: Column("manufacturer")] string Manufacturer,
    [property: Column("price")] decimal Price,
    [property: Column("stock_quantity")] int StockQuantity,
    [property: Column("is_delete")] bool isDelete
);
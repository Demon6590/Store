using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

const string ConnectionString = @"Data Source=C:\Users\college\RiderProjects\Store\store.db;";

var db = new ProductContext(ConnectionString);
Console.WriteLine(db.AddProduct(new Product("r4fff","апельсин","Дерево",120,45,false)));

var list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine(db.UpdateProduct(new Product("r4fff","апельсин","Дерево",120,42,false)));
list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}

list = db.GetByName("апельсин").ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
list = (List<Product>)db.GetByPrice(120).ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
list = db.GetByStock(45).ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine(db.SoftDelete("r4fff"));
list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}

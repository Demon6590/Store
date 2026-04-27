using System;
using System.Collections.Generic;
using System.Linq;
using Store.Model;

const string ConnectionString = @"Data Source=C:\Users\college\RiderProjects\Store\store.db;";

var db = new ProductContext(ConnectionString);
db.AddProduct(new Product("r52ff","мандарин","Дерево",134,45,false));

var list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine("---------");

db.UpdateProduct(new Product("r52ff","апельсин","Дерево",120,10,false));
list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine("---------");

list = db.GetByName("апельсин").ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine("---------");

list = (List<Product>)db.GetByPrice(120).ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
Console.WriteLine("---------");

list = db.GetByStock(45).ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}
db.SoftDelete("r52ff");
Console.WriteLine("---------");
list = db.GetAll().ToList();
foreach (var product in list)
{
    Console.WriteLine(product);
}

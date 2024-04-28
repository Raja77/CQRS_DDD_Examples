using System;
using System.Collections.Generic;
using System.Linq;

// Command and Query Interfaces
public interface ICommand
{
    void Execute();
}

public interface IQuery<T>
{
    T Execute();
}

// Commands
public class AddProductCommand : ICommand
{
    private readonly Product product;

    public AddProductCommand(Product product)
    {
        this.product = product;
    }

    public void Execute()
    {
        ProductDatabase.Products.Add(product);
    }
}

// Queries
public class GetAllProductsQuery : IQuery<List<Product>>
{
    public List<Product> Execute()
    {
        return ProductDatabase.Products;
    }
}

// Handlers
public class ProductCommandHandler
{
    public void Handle(ICommand command)
    {
        command.Execute();
    }
}

public class ProductQueryHandler
{
    public T Handle<T>(IQuery<T> query)
    {
        return query.Execute();
    }
}

// Domain Model
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

// Simulated Database
public static class ProductDatabase
{
    public static List<Product> Products = new List<Product>();
}

// Example Usage
class Program
{
    static void Main(string[] args)
    {
        var commandHandler = new ProductCommandHandler();
        var queryHandler = new ProductQueryHandler();

        // Adding a product
        var product = new Product { Id = 1, Name = "Laptop", Price = 1200 };
        var addProductCommand = new AddProductCommand(product);
        commandHandler.Handle(addProductCommand);

        // Querying products
        var getAllProductsQuery = new GetAllProductsQuery();
        var products = queryHandler.Handle(getAllProductsQuery);

        foreach (var prod in products)
        {
            Console.WriteLine($"Product: {prod.Name}, Price: {prod.Price}");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

// Define interfaces for command and query functionalities
public interface ICommand
{
    void Execute(); // Execute an action
}

public interface IQuery<T>
{
    T Execute(); // Execute a query and return a type T
}

// Command to add a product to the database
public class AddProductCommand : ICommand
{
    private readonly Product product; // Encapsulated product data

    public AddProductCommand(Product product)
    {
        this.product = product; // Constructor initializes the product
    }

    public void Execute()
    {
        ProductDatabase.Products.Add(product); // Execute command by adding the product to the database
    }
}

// Query to retrieve all products from the database
public class GetAllProductsQuery : IQuery<List<Product>>
{
    public List<Product> Execute()
    {
        return ProductDatabase.Products; // Return the list of all products
    }
}

// Handler for commands
public class ProductCommandHandler
{
    public void Handle(ICommand command)
    {
        command.Execute(); // Execute the given command
    }
}

// Handler for queries
public class ProductQueryHandler
{
    public T Handle<T>(IQuery<T> query)
    {
        return query.Execute(); // Execute the given query and return the result
    }
}

// Domain model for Product
public class Product
{
    public int Id { get; set; } // Product identifier
    public string Name { get; set; } // Product name
    public decimal Price { get; set; } // Product price
}

// Simulated database containing a list of products
public static class ProductDatabase
{
    public static List<Product> Products = new List<Product>(); // Static list of products
}

// Main program class
class Program
{
    static void Main(string[] args)
    {
        var commandHandler = new ProductCommandHandler(); // Instantiate a command handler
        var queryHandler = new ProductQueryHandler(); // Instantiate a query handler

        // Create and add a product
        var product = new Product { Id = 1, Name = "Laptop", Price = 1200 };
        var addProductCommand = new AddProductCommand(product);
        commandHandler.Handle(addProductCommand); // Handle command to add the product

        // Retrieve and display all products
        var getAllProductsQuery = new GetAllProductsQuery();
        var products = queryHandler.Handle(getAllProductsQuery); // Handle query to get all products

        foreach (var prod in products)
        {
            Console.WriteLine($"Product: {prod.Name}, Price: {prod.Price}"); // Output product details
        }
    }
}


/*
Description of the CQRS pattern

Explanation:
Interfaces (ICommand and IQuery<T>): These are designed to define a contract for all command and query types. ICommand encapsulates the action of executing a command, whereas IQuery<T> encapsulates the retrieval of data and returns a specified type T.
Command Implementation (AddProductCommand): This class implements the ICommand interface, taking a Product as an argument. It defines how the Product should be added to the database when the command is executed.
Query Implementation (GetAllProductsQuery): This class implements the IQuery<List<Product>> interface and returns a list of all products in the database when executed.
Handlers (ProductCommandHandler and ProductQueryHandler): These classes are responsible for taking an instance of a command or query and executing it. This abstraction allows the details of command/query execution to be managed independently of the caller.
Domain Model (Product): Represents the data structure of a product in the system.
Simulated Database (ProductDatabase): A static class that simulates a database by holding a list of products.
Main Program Flow: In the Main method, a product is created, added using a command, and then all products are queried and displayed. This demonstrates a basic usage scenario of CQRS where the command and query responsibilities are cleanly separated.


*/

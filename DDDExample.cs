/*This example demonstrates a DDD approach applied to an Inventory Management System.*/

using System;
using System.Collections.Generic;

// Entity Class
public class InventoryItem
{
    // Properties of the InventoryItem
    public Guid Id { get; private set; } // Unique identifier for the inventory item.
    public string Name { get; private set; } // Name of the inventory item.
    public int Quantity { get; private set; } // Quantity of the inventory item in stock.

    // Constructor for creating a new inventory item with a unique ID, name, and initial quantity.
    public InventoryItem(string name, int quantity)
    {
        Id = Guid.NewGuid(); // Automatically generate a new unique identifier.
        Name = name; // Set the name of the item.
        Quantity = quantity; // Set the initial quantity of the item.
    }

    // Method to add stock to the inventory item.
    public void AddStock(int quantity)
    {
        if (quantity > 0) // Check if the quantity to add is positive.
        {
            Quantity += quantity; // Increase the stock quantity.
        }
    }

    // Method to remove stock from the inventory item.
    public void RemoveStock(int quantity)
    {
        if (quantity > 0 && quantity <= Quantity) // Check if the quantity to remove is positive and available.
        {
            Quantity -= quantity; // Decrease the stock quantity.
        }
        else
        {
            throw new InvalidOperationException("Insufficient stock."); // Throw an exception if stock is insufficient.
        }
    }
}

// Repository Interface
public interface IInventoryRepository
{
    void Add(InventoryItem item); // Add an inventory item to the repository.
    InventoryItem GetById(Guid id); // Retrieve an inventory item by its ID.
    void Save(); // Save changes made to the repository.
}

// Implementation of the Repository Interface
public class InventoryRepository : IInventoryRepository
{
    private List<InventoryItem> _inventoryItems = new List<InventoryItem>(); // Internal list to hold inventory items.

    // Add an inventory item to the repository.
    public void Add(InventoryItem item)
    {
        _inventoryItems.Add(item); // Add the new item to the internal list.
    }

    // Retrieve an inventory item by its ID.
    public InventoryItem GetById(Guid id)
    {
        return _inventoryItems.Find(item => item.Id == id); // Find the item by ID and return it.
    }

    // Save changes made to the repository.
    public void Save()
    {
        // In a real-world application, this method would handle persistence to a database.
        // For demonstration purposes, this example assumes that changes are automatically saved.
    }
}

// Application Service to manage inventory
public class InventoryService
{
    private IInventoryRepository _inventoryRepository; // Dependency on the repository interface.

    // Constructor with dependency injection.
    public InventoryService(IInventoryRepository inventoryRepository)
    {
        _inventoryRepository = inventoryRepository; // Initialize the repository.
    }

    // Method to add a new inventory item.
    public void CreateInventoryItem(string name, int quantity)
    {
        var newItem = new InventoryItem(name, quantity); // Create a new inventory item.
        _inventoryRepository.Add(newItem); // Add the new item to the repository.
        _inventoryRepository.Save(); // Save changes to the repository.
    }

    // Method to add stock to an existing item.
    public void AddStock(Guid itemId, int quantity)
    {
        InventoryItem item = _inventoryRepository.GetById(itemId); // Retrieve the item by ID.
        if (item != null)
        {
            item.AddStock(quantity); // Add stock to the item.
            _inventoryRepository.Save(); // Save changes to the repository.
        }
        else
        {
            throw new KeyNotFoundException("Item not found."); // Throw an exception if the item is not found.
        }
    }

    // Method to remove stock from an existing item.
    public void RemoveStock(Guid itemId, int quantity)
    {
        InventoryItem item = _inventoryRepository.GetById(itemId); // Retrieve the item by ID.
        if (item != null)
        {
            item.RemoveStock(quantity); // Remove stock from the item.
            _inventoryRepository.Save(); // Save changes to the repository.
        }
        else
        {
            throw new KeyNotFoundException("Item not found."); // Throw an exception if the item is not found.
        }
    }
}





/*Explanation*/
/*
1.# InventoryItem Class (Entity)
This class represents the core domain entity in the inventory system. It contains:

Properties: Id (unique identifier), Name (name of the item), and Quantity (current stock level).
Constructor: Initializes a new item with a unique Id, Name, and Quantity.
Methods:
AddStock(int quantity): Adds a specified quantity to the item's stock if the quantity is positive.
RemoveStock(int quantity): Subtracts a specified quantity from the item's stock if the quantity is available; otherwise, it throws an exception.
2.# IInventoryRepository Interface (Repository Interface)
This interface outlines the contract for the repository which will handle data operations for InventoryItem entities. It includes methods to:

Add(InventoryItem item): Adds a new inventory item to the repository.
GetById(Guid id): Retrieves an inventory item by its unique identifier.
Save(): Persists changes to the repository.
3.# InventoryRepository Class (Repository Implementation)
Implements the IInventoryRepository interface with:

Private Field: _inventoryItems, a list that simulates a database for storing inventory items.
Methods:
Add(InventoryItem item): Adds an inventory item to the internal list.
GetById(Guid id): Finds an item in the list by its ID.
Save(): This method is a placeholder, simulating the saving of changes to a data store.
4.# InventoryService Class (Application Service)
This class represents the application layer, where business logic is applied using the domain model and repository. It includes:

Private Field: _inventoryRepository, an instance of IInventoryRepository for data operations.
Constructor: Accepts an IInventoryRepository to facilitate dependency injection.
Methods:
CreateInventoryItem(string name, int quantity): Creates and adds a new inventory item.
AddStock(Guid itemId, int quantity): Adds stock to an existing item if found; otherwise throws an exception.
RemoveStock(Guid itemId, int quantity): Removes stock from an existing item if the stock is available and the item exists; otherwise throws an exception.
Summary of the DDD Approach
Domain Model: InventoryItem directly models the domain entity and includes behavior (methods) related to the entity itself.
Repository Pattern: IInventoryRepository and InventoryRepository abstract and implement data access respectively, allowing the domain model to remain isolated from data access concerns.
Service Layer: InventoryService provides a higher-level interface used by external clients to interact with domain entities while encapsulating business rules and transactions.
*/

/*This example demonstrates a DDD approach applied to an inventory management system.*/

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
    private readonly List

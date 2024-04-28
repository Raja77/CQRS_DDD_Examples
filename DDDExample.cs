/*This example demonstrates a DDD approach applied to an inventory management system.*/

using System;

// Entity
public class InventoryItem
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }

    public InventoryItem(string name, int quantity)
    {
        Id = Guid.NewGuid();
        Name = name;
        Quantity = quantity;
    }

    public void AddStock(int quantity)
    {
        if (quantity > 0)
        {
            Quantity += quantity;
        }
    }

    public void RemoveStock(int quantity)
    {
        if (quantity > 0 && quantity <= Quantity)
        {
            Quantity -= quantity;
        }
        else
        {
            throw new InvalidOperationException("Insufficient stock.");
        }
    }
}

// Repository Interface
public interface IInventoryRepository
{
    void Add(InventoryItem item);
    InventoryItem GetById(Guid id);
    void Save();
}

// Implementation of Repository
public class InventoryRepository : IInventoryRepository
{
    private readonly List

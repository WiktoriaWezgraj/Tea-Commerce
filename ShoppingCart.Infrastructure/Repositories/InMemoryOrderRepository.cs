

using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Infrastructure.Repositories;

public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = new();

    public Task<Order> GetByIdAsync(int orderId) =>
        Task.FromResult(_orders.FirstOrDefault(o => o.OrderId == orderId));

    public Task<IEnumerable<Order>> GetAllAsync() =>
        Task.FromResult<IEnumerable<Order>>(_orders);

    public Task AddAsync(Order order)
    {
        // Simple Id generation
        order.OrderId = _orders.Count == 0 ? 1 : _orders.Max(o => o.OrderId) + 1;
        _orders.Add(order);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Order order)
    {
        var existing = _orders.FirstOrDefault(o => o.OrderId == order.OrderId);
        if (existing != null)
        {
            _orders.Remove(existing);
            _orders.Add(order);
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int orderId)
    {
        var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
        if (order != null) _orders.Remove(order);
        return Task.CompletedTask;
    }
}


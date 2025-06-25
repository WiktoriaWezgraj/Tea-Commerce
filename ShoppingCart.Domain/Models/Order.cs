
using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Domain.Models;
public class Order
{
    public int OrderId { get; set; }          
    public int CartId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = new List<OrderItem>();
    public decimal TotalAmount { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
}

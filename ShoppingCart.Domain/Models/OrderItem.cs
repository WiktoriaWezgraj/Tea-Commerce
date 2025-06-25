

namespace ShoppingCart.Domain.Models;
public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }

    public decimal TotalPrice => Quantity * Price;
}
namespace ShoppingCart.Domain.Models;
public class CartItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
}

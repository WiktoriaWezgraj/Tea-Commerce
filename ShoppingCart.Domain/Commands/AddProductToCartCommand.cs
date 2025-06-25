using MediatR;

namespace ShoppingCart.Domain.Commands;
public class AddProductToCartCommand : IRequest
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal Price { get; set; }
}
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Commands;
using ShoppingCart.Domain.Interfaces;
using MediatR;

namespace ShoppingCart.Application.CommandHandlers;

public class AddProductToCartCommandHandler : IRequestHandler<AddProductToCartCommand>
{
    private readonly ICartAdder _cartAdder;

    public AddProductToCartCommandHandler(ICartAdder cartAdder)
    {
        _cartAdder = cartAdder;
    }

    public Task Handle(AddProductToCartCommand command, CancellationToken cancellationToken)
    {
        var quantity = command.Quantity > 0 ? command.Quantity : 1;

        var product = new Product
        {
            ProductId = command.ProductId,
            Price = command.Price
        };
        _cartAdder.AddProductToCart(command.CartId, product, quantity);
        return Task.CompletedTask;
    }
}
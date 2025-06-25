using MediatR;
using ShoppingCart.Domain.Commands;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Domain.Enums;

namespace ShoppingCart.Application.CommandHandlers;

public class ProcessCartHandler : IRequestHandler<ProcessCartCommand, Order>
{
    private readonly ICartReader _cartReader;
    private readonly IOrderRepository _orderRepository;

    public ProcessCartHandler(ICartReader cartReader, IOrderRepository orderRepository)
    {
        _cartReader = cartReader;
        _orderRepository = orderRepository;
    }

    public async Task<Order> Handle(ProcessCartCommand request, CancellationToken cancellationToken)
    {
        var cart = _cartReader.GetCart(request.CartId);
        if (cart == null)
            throw new Exception("Cart not found");

        // Walidacja koszyka
        if (!cart.Items.Any())
            throw new Exception("Cart is empty");

        // Tworzenie zamówienia
        var order = new Order
        {
            Items = cart.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList(),
            CreatedAt = DateTime.UtcNow,
            Status = OrderStatus.Pending,
        };

        await _orderRepository.AddAsync(order);

        // Zwrot danych o zamówieniu
        return order;
    }
}

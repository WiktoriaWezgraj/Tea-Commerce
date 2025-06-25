using MediatR;
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Commands;
public class ProcessCartCommand : IRequest<Order>
{
    public int CartId { get; set; }
}


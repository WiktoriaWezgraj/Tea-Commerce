using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingCart.Domain.Queries;
using ShoppingCart.Domain.Commands;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Queries;
using ShoppingCart.Application.Services;
using ShoppingCart.Domain.Enums;
using ShoppingCart.Domain.Models;


namespace ShoppingCart.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class CartController : ControllerBase
{

    private readonly IMediator _mediator;
    private readonly IInvoiceService _invoiceService;

    public CartController(IMediator mediator, IInvoiceService invoiceService)
    {
        _mediator = mediator;
        _invoiceService = invoiceService;
    }

    [HttpPost("add-product")]
    public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartCommand command)
    {
        var userId = 1;

        await _mediator.Send(command);
        return Ok();
    }

    [HttpPost("remove-product")]
    public async Task<IActionResult> RemoveProductFromCart([FromBody] RemoveProductFromCartCommand command)
    {
        await _mediator.Send(command);
        return Ok();
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllCarts()
    {
        var query = new GetAllCartsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpGet("{cartId}")]
    public async Task<IActionResult> GetCart(int cartId)
    {
        var query = new GetCartQuery { CartId = cartId };
        var result = await _mediator.Send(query);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessCart([FromBody] ProcessCartCommand command)
    {
        try
        {
            var order = await _mediator.Send(command);
            return Ok(new
            {
                orderId = order.OrderId,
                createdAt = order.CreatedAt,
                status = order.Status.ToString(),
                totalAmount = order.Items.Sum(i => i.Quantity * i.Price),
                items = order.Items.Select(i => new
                {
                    productId = i.ProductId,
                    quantity = i.Quantity,
                    Price = i.Price,
                    totalPrice = i.Quantity * i.Price
                }),
                message = "Order placed successfully."
            });
        }
        catch (System.Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPost("send-invoice")]
    public async Task<IActionResult> SendInvoice([FromBody] SendInvoiceRequest request)
    {
        try
        {
            var cartRepo = HttpContext.RequestServices.GetRequiredService<ICartRepository>();
            var cart = cartRepo.FindById(request.CartId);

            if (cart == null || !cart.Items.Any())
                return NotFound(new { message = "Cart not found or empty." });

            var order = new Order
            {
                CartId = cart.Id,
                CreatedAt = DateTime.UtcNow,
                Status = OrderStatus.Pending,
                Items = cart.Items.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList()
            };

            await _invoiceService.GenerateAndSendInvoiceAsync(order, request.Email);

            return Ok(new { message = $"Invoice for cart {request.CartId} sent to {request.Email}" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}



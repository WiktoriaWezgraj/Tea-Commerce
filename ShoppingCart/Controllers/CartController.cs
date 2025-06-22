﻿using Microsoft.AspNetCore.Mvc;
using MediatR;
using ShoppingCart.Domain.Queries;
using ShoppingCart.Domain.Commands;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IMediator _mediator;

        public CartController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProductToCart([FromBody] AddProductToCartCommand command)
        {
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

        [HttpGet("values")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCart(int cartId)
        {
            var query = new GetCartQuery { CartId = cartId };
            var result = await _mediator.Send(query);
            return result == null ? NotFound() : Ok(result);
        }
    }
}


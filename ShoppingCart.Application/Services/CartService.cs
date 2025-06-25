using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Domain.Models;
using ShoppingCart.Infrastructure.Repositories;

namespace ShoppingCart.Application.Services;
public class CartService : ICartAdder, ICartRemover, ICartReader
{
    private readonly ICartRepository _repository;

    public CartService(ICartRepository repository)
    {
        _repository = repository;
    }

    public void AddProductToCart(int cartId, Product product, int quantity = 1)
    {
        var cart = _repository.FindById(cartId) ?? new Cart { Id = cartId };
        var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == product.ProductId);
        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            cart.Items.Add(new CartItem
            {
                ProductId = product.ProductId,
                Quantity = quantity,
                Price = product.Price 
            });
        }

        _repository.Add(cart);
    }

    public void RemoveProductFromCart(int cartId, int productId)
    {
        var cart = _repository.FindById(cartId);
        if (cart != null)
        {
            var cartItem = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (cartItem != null)
            {
                cart.Items.Remove(cartItem);
                _repository.Update(cart);
            }
        }
    }

    public Cart GetCart(int cartId)
    {
        var cart = _repository.FindById(cartId);
        if (cart == null) return null;

        return new Cart
        {
            Id = cart.Id,
            Items = cart.Items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        };
    }

    public List<Cart> GetAllCarts()
    {
        return _repository.GetAll().Select(c => new Cart
        {
            Id = c.Id,
            Items = c.Items.Select(i => new CartItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList()
        }).ToList();
    }
}
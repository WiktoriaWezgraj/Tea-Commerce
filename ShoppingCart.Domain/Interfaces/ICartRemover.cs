//using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces;
public interface ICartRemover
{
    void RemoveProductFromCart(int cartId, int productId);
}
using ShoppingCart.Domain.Models;

namespace ShoppingCart.Domain.Interfaces;
public interface ICartRepository
{
    void Add(Cart cart);
    void Update(Cart cart);
    Cart FindById(int id);
    List<Cart> GetAll();
}
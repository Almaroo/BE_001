using BE_001.Web.Models;

namespace BE_001.Web.Services.Cart;

public interface ICartService
{
    IEnumerable<Item> GetItems();
    void AddToCart(Item item);
    void RemoveFromCart(Item item);
    void ClearCart();
    int ItemsCount();
}
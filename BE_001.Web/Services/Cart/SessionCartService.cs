using BE_001.Web.Helpers;
using BE_001.Web.Models;

namespace BE_001.Web.Services.Cart;

public class SessionCartService : ICartService
{
    private const string SessionsCartItemsKey = "cart-items";
    private readonly ISession _session;

    public SessionCartService(IHttpContextAccessor contextAccessor)
    {
        _session = contextAccessor.HttpContext.Session;
    }
    
    public IEnumerable<Item> GetItems() => _session.GetJson<List<Item>>(SessionsCartItemsKey) ?? new List<Item>();

    public void AddToCart(Item item)
    {
        var items = GetItems().ToList();
        items.Add(item);
        _session.AddJson(SessionsCartItemsKey, items);
    }

    public void RemoveFromCart(Item item)
    {
        var items = GetItems().ToList();
        items.Remove(items.FirstOrDefault(x => x.ItemId == item.ItemId));
        _session.AddJson(SessionsCartItemsKey, items);
    }

    public void ClearCart()
    {
        _session.AddJson(SessionsCartItemsKey, new List<Item>());
    }

    public int ItemsCount() => GetItems().Count();
}
using BE_001.Web.Models;
using BE_001.Web.Specifications;

namespace BE_001.Web.Repositories.Items;

public interface IItemRepository
{
    Task<IEnumerable<Item>> GetItems(ISpecification<Item>? specification);
    Task<Item> GetItem(int itemId);
    Task UpdateItem(Item item);
    Task CreateItem(Item item);
    Task DeleteItem(int itemId);
}
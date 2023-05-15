using BE_001.Web.Models;
using BE_001.Web.Specifications;

namespace BE_001.Web.Services.Browsing;

public interface IBrowsingService
{
    Task<IEnumerable<Item>> GetAllItems(ISpecification<Item> specification);
    Task<Item> GetItem(int itemId);
}
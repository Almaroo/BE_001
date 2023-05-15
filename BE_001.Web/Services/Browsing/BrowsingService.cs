using BE_001.Web.Models;
using BE_001.Web.Repositories.Items;
using BE_001.Web.Specifications;

namespace BE_001.Web.Services.Browsing;

public class BrowsingService : IBrowsingService
{
    private readonly IItemRepository _itemRepository;

    public BrowsingService(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<Item>> GetAllItems(ISpecification<Item> specification = null) 
        => await _itemRepository.GetItems(specification);

    public async Task<Item> GetItem(int itemId) => await _itemRepository.GetItem(itemId);
}
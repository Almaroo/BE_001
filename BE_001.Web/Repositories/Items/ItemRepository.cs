using BE_001.Web.DbContexts;
using BE_001.Web.Models;
using BE_001.Web.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BE_001.Web.Repositories.Items;

public class ItemRepository : IItemRepository
{
    private readonly ShopDbContext _dbContext;
    private readonly ILogger<ItemRepository> _logger;

    public ItemRepository(ShopDbContext dbContext, ILogger<ItemRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<Item>> GetItems(ISpecification<Item>? specification)
    {
        specification ??= new AllItemsSpecification();
        return await _dbContext.Items.AsNoTracking().Where(specification.ToExpression()).ToListAsync();
    }

    public async Task<Item> GetItem(int itemId) => await _dbContext.Items.FirstOrDefaultAsync(i => i.ItemId == itemId);

    public async Task UpdateItem(Item item)
    {
        var oldItem = await GetItem(item.ItemId);

        oldItem.Description = item.Description;
        oldItem.Name = item.Name;
        oldItem.Quantity = item.Quantity;
        oldItem.Price = item.Price;

        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateItem(Item item)
    {
        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteItem(int itemId)
    {
        _dbContext.Remove(new Item { ItemId = itemId });
        await _dbContext.SaveChangesAsync();
    }
}
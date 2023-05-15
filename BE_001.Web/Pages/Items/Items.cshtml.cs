using BE_001.Web.Models;
using BE_001.Web.Services.Browsing;
using BE_001.Web.Services.Rating;
using Microsoft.AspNetCore.Mvc;

namespace BE_001.Web.Pages.Items;

public class ItemsModel : DI_BassePageModel
{
    [BindProperty] IEnumerable<Item> Items { get; set; } = new List<Item>();

    public ItemsModel(IBrowsingService browsingService, IRatingService ratingService) : base(browsingService, ratingService)
    {
    }

    public async Task OnGetAsync()
    {
        Items = await BrowsingService.GetAllItems(null);
    }
}
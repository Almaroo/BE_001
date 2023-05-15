using BE_001.Web.Models;
using BE_001.Web.Services.Browsing;
using BE_001.Web.Services.Rating;
using Microsoft.AspNetCore.Mvc;

namespace BE_001.Web.Pages.Items;

public class ItemModel : DI_BassePageModel
{
    public ItemModel(IBrowsingService browsingService, IRatingService ratingService) : base(browsingService, ratingService)
    {
    }

    [BindProperty(SupportsGet = true)]
    public int ItemId { get; set; }

    public Item Item { get; set; }
    
    public void OnGet()
    {
        
    }
}
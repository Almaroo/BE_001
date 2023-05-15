using BE_001.Web.Models;
using BE_001.Web.Repositories.Items;
using BE_001.Web.Services.Browsing;
using BE_001.Web.Services.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BE_001.Web.Areas.Cart.Pages.Cart;

public class CheckOutModel : PageModel
{
    private readonly IBrowsingService _browsingService;
    private readonly ICartService _cartService;

    public CheckOutModel(IBrowsingService browsingService, ICartService cartService)
    {
        _browsingService = browsingService;
        _cartService = cartService;
    }

    [BindProperty] public IEnumerable<Item> Items { get; set; } = new List<Item>();

    [BindProperty] public int ItemId { get; set; }
    
    [BindProperty] public string ReturnUrl { get; set; }
    
    public void OnGet()
    {
        Items = _cartService.GetItems().ToList();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        _cartService.AddToCart(await _browsingService.GetItem(ItemId));
        return LocalRedirect(ReturnUrl ?? "~/");
    }

    public IActionResult OnPostCheckout()
    {
        // add order creation here
        
        _cartService.ClearCart();
        return LocalRedirect("~/Cart/Cart/Confirmation");
    }
}
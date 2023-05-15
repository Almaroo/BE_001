using System.Linq.Expressions;
using BE_001.Web.Models;
using BE_001.Web.Services.Browsing;
using BE_001.Web.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BE_001.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IBrowsingService _browsingService;

    [BindProperty]
    public IEnumerable<Item> FeaturedItems { get; private set; } = new List<Item>();

    public IndexModel(ILogger<IndexModel> logger, IBrowsingService browsingService)
    {
        _logger = logger;
        _browsingService = browsingService;
    }

    public async Task OnGetAsync()
    {
        FeaturedItems = await _browsingService.GetAllItems(new FirstNItemsSpecification(10));
    }

    private class FirstNItemsSpecification : Specification<Item>
    {
        private readonly int _count;

        public FirstNItemsSpecification(int count)
        {
            _count = count;
        }

        public override Expression<Func<Item, bool>> ToExpression() => item => item.ItemId <= _count;
    }
}
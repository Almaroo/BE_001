using BE_001.Web.Services.Browsing;
using BE_001.Web.Services.Rating;

namespace BE_001.Web.Pages.Items;

public abstract class DI_BassePageModel
{
    protected readonly IBrowsingService BrowsingService;
    protected readonly IRatingService RatingService;

    protected DI_BassePageModel(IBrowsingService browsingService, IRatingService ratingService)
    {
        BrowsingService = browsingService;
        RatingService = ratingService;
    }
}
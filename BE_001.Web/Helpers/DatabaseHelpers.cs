using BE_001.Web.DbContexts;
using BE_001.Web.Models;
using Bogus;
using Microsoft.EntityFrameworkCore;

namespace BE_001.Web.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public static class DatabaseHelpers
{
    public static void MigrateDatabase(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        var services = scope.ServiceProvider;
        using var context = services.GetRequiredService<ShopDbContext>();
        try
        {
            context.Database.Migrate();
            SeedDatabase(context);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger>();
            logger.LogError(ex, "An error occurred while migrating the database.");

            throw;
        }
    }

    private static void SeedDatabase(ShopDbContext dbContext)
    {
        const int seed = 12345;
        Randomizer.Seed = new Random(seed);
        
        if (!dbContext.Items.Any()) 
            SeedItems(dbContext);

        if(!dbContext.Reviews.Any())
            SeedReviews(dbContext);
        
        dbContext.SaveChanges();
    }

    private static void SeedItems(ShopDbContext dbContext)
    {
        const int items_count = 100;
        
        var generationRules = new Faker<Item>("pl")
            .RuleFor(x => x.ImgUrl, f => f.Image.PicsumUrl())
            .RuleFor(x => x.Name, f => f.Commerce.Product())
            .RuleFor(x => x.Description, f => f.Commerce.ProductDescription())
            .RuleFor(x => x.Quantity, f => f.Random.Int(0, 15))
            .RuleFor(x => x.Price, f => f.Random.Decimal(0, 100));
        
        dbContext.Items.AddRange(generationRules.Generate(items_count));
    }

    private static void SeedReviews(ShopDbContext dbContext)
    {
        const int reviewsCount = 175;

        var generation_rules = new Faker<Review>("pl")
            .RuleFor(x => x.UserName, f => f.Internet.UserName())
            .RuleFor(x => x.ItemId, f => f.Random.Int(1, 100))
            .RuleFor(x => x.Rating, f => f.Random.Int(1, 5))
            .RuleFor(x => x.Description, f => f.Rant.Review());
        
        dbContext.AddRange(generation_rules.Generate(reviewsCount));
    }
}

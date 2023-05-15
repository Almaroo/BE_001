using BE_001.Web.DbContexts;
using BE_001.Web.Models;
using BE_001.Web.Repositories.ReviewRepository;
using BE_001.Web.Specifications;
using Microsoft.EntityFrameworkCore;

namespace BE_001.Web.Repositories.Reviews;

public class ReviewRepository : IReviewRepository
{
    private readonly ShopDbContext _dbContext;

    public ReviewRepository(ShopDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Review>> GetAllReviews(ISpecification<Review>? specification)
    {
        specification ??= new AllReviewsSpecification();
        return await _dbContext.Reviews.AsNoTracking().Where(specification.ToExpression()).ToListAsync();
    }

    public async Task<Review> GetReview(int reviewId)
    {
        return await _dbContext.Reviews.FirstOrDefaultAsync(r => r.ReviewId == reviewId);
    }

    public async Task UpdateReview(Review review)
    {
        var oldReview = await GetReview(review.ReviewId);
        oldReview.Description = review.Description;
        oldReview.Rating = review.Rating;
        oldReview.UserName = review.UserName;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteReview(int reviewId)
    {
        _dbContext.Reviews.Remove(new Review { ReviewId = reviewId });
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateReview(Review review)
    {
        await _dbContext.Reviews.AddAsync(review);
        await _dbContext.SaveChangesAsync();
    }
}
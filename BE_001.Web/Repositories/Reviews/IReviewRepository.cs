using BE_001.Web.Models;
using BE_001.Web.Specifications;

namespace BE_001.Web.Repositories.ReviewRepository;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllReviews(ISpecification<Review>? specification);
    Task<Review> GetReview(int reviewId);
    Task UpdateReview(Review review);
    Task DeleteReview(int reviewId);
    Task CreateReview(Review review);
}
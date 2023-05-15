using BE_001.Web.Models;
using BE_001.Web.Repositories.ReviewRepository;

namespace BE_001.Web.Services.Rating;

public class RatingService : IRatingService
{
    private readonly IReviewRepository _reviewRepository;

    public RatingService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    public async Task PlaceRating(int rating, string description, int itemId, string userName)
    {
        await _reviewRepository.CreateReview(new Review
        {
            Rating = rating,
            Description = description,
            ItemId = itemId,
            UserName = userName
        });
    }
}
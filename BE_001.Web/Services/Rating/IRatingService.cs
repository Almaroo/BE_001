namespace BE_001.Web.Services.Rating;

public interface IRatingService
{
    Task PlaceRating(int rating, string description, int itemId, string userName);
}
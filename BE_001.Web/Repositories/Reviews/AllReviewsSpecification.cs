using System.Linq.Expressions;
using BE_001.Web.Models;
using BE_001.Web.Specifications;

namespace BE_001.Web.Repositories.ReviewRepository;

public class AllReviewsSpecification : Specification<Review>
{
    public override Expression<Func<Review, bool>> ToExpression() => _ => true;
}
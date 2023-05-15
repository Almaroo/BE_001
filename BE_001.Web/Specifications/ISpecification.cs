using System.Linq.Expressions;

namespace BE_001.Web.Specifications;

public interface ISpecification<T>
{
    bool IsSatisfiedBy(T obj);
    Expression<Func<T, bool>> ToExpression();
}

using System.Linq.Expressions;
using BE_001.Web.Models;
using BE_001.Web.Specifications;

namespace BE_001.Web.Repositories.Items;

public class AllItemsSpecification : Specification<Item>
{
    public override Expression<Func<Item, bool>> ToExpression() => _ => true;
}
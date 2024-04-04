using Ardalis.Specification;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.ApplicationCore.Specifications;
public class OrderSpecifications : Specification<Order>
{
    public OrderSpecifications()
    {
        Query
            .Include(o => o.OrderItems)
            .Include(o => o.Status);
    }
}

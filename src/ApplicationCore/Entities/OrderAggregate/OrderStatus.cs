using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
public class OrderStatus : BaseEntity, IAggregateRoot
{
    public string Status { get; set; }
}

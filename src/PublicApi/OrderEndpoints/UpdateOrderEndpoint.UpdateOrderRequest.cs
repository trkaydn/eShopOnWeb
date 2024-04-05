namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class UpdateOrderRequest : BaseRequest
{
    public int Id { get; set; }
    public OrderStatusDto Status { get; set; }
}

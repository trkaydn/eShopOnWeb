namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderItemDto
{
    public int Id { get; set; }
    public decimal UnitPrice { get; private set; }
    public int Units { get; private set; }
    public string ProductName { get; private set; }
}

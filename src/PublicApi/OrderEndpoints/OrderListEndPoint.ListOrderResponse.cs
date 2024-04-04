using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using System.Collections.Generic;
using System;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;

namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class ListOrderResponse : BaseResponse
{
    public ListOrderResponse(Guid correlationId) : base(correlationId)
    {
    }

    public ListOrderResponse()
    {
    }

    public List<OrderDto> Orders { get; set; } = new List<OrderDto>();
}

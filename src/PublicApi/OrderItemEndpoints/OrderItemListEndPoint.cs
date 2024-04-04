using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using Azure.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.CatalogBrandEndpoints;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using MinimalApi.Endpoint;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderItemListEndPoint : IEndpoint<IResult, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public OrderItemListEndPoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/order-items",
            async (IRepository<Order> orderRepository) =>
            {
                return await HandleAsync(orderRepository);
            })
           .Produces<ListOrderResponse>()
           .WithTags("OrderItemEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Order> orderRepository)
    {
       var response = new ListOrderResponse();
        var specification = new OrderItemSpecifications();

        var items = await orderRepository.ListAsync(specification);

        response.OrderItems.AddRange(items.Select(_mapper.Map<OrderItemDto>));

        return Results.Ok(response);
    }
}

using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using MinimalApi.Endpoint;
namespace Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;

public class OrderListEndPoint : IEndpoint<IResult, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public OrderListEndPoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
            (IRepository<Order> orderRepository) =>
            {
                return await HandleAsync(orderRepository);
            })
           .Produces<ListOrderResponse>()
           .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(IRepository<Order> orderRepository)
    {
        var response = new ListOrderResponse();
        var specification = new OrderSpecifications();

        var items = await orderRepository.ListAsync(specification);

        response.Orders.AddRange(items.OrderByDescending(x => x.OrderDate).Select(_mapper.Map<OrderDto>));

        return Results.Ok(response);
    }
}

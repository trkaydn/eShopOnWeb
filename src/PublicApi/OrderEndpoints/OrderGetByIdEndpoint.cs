using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using MinimalApi.Endpoint;

namespace Microsoft.eShopWeb.PublicApi.OrderEndpoints;

public class OrderGetByIdEndpoint : IEndpoint<IResult, GetByIdOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;

    public OrderGetByIdEndpoint(IMapper mapper)
    {
        _mapper = mapper;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapGet("api/orders/detail/{orderId}",
            [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async
           (int orderId, IRepository<Order> itemRepository) =>
           {
               return await HandleAsync(new GetByIdOrderRequest(orderId), itemRepository);
           })
           .Produces<GetByIdOrderResponse>()
           .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(GetByIdOrderRequest request, IRepository<Order> itemRepository)
    {
        var spec = new OrderWithItemsByIdSpec(request.OrderId);

        var response = new GetByIdOrderResponse(request.CorrelationId());

        var item = await itemRepository.GetBySpecAsync(spec);
        if (item is null)
            return Results.NotFound();

        response.Order = _mapper.Map<OrderDto>(item);
        return Results.Ok(response);
    }
}

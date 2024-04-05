using System.Collections.Generic;
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

public class UpdateOrderEndpoint : IEndpoint<IResult, UpdateOrderRequest, IRepository<Order>>
{
    private readonly IMapper _mapper;
    private readonly IRepository<OrderStatus> _orderStatusRepository;

    public UpdateOrderEndpoint(IMapper mapper, IRepository<OrderStatus> orderStatusRepository)
    {
        _mapper = mapper;
        _orderStatusRepository = orderStatusRepository;
    }

    public void AddRoute(IEndpointRouteBuilder app)
    {
        app.MapPut("api/orders",
              [Authorize(Roles = BlazorShared.Authorization.Constants.Roles.ADMINISTRATORS, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        async
              (UpdateOrderRequest request, IRepository<Order> itemRepository) =>
              {
                  return await HandleAsync(request, itemRepository);
              })
              .Produces<UpdateOrderResponse>()
              .WithTags("OrderEndpoints");
    }

    public async Task<IResult> HandleAsync(UpdateOrderRequest request, IRepository<Order> itemRepository)
    {
        var response = new UpdateOrderResponse(request.CorrelationId());
        var spec = new OrderWithItemsByIdSpec(request.Id);

        var existingItem = await itemRepository.GetBySpecAsync(spec);
        if (existingItem == null)
        {
            return Results.NotFound();
        }

        var approvedStatus = await _orderStatusRepository.GetByIdAsync(2);
        existingItem.UpdateStatus(approvedStatus);

        await itemRepository.UpdateAsync(existingItem);

        var dto = new OrderDto
        {
            Id = existingItem.Id,
            BuyerId = existingItem.BuyerId,
            OrderDate = existingItem.OrderDate,
            OrderItems = _mapper.Map<List<OrderItemDto>>(existingItem.OrderItems),
            Status = _mapper.Map<OrderStatusDto>(existingItem.Status),
            Total = existingItem.Total()
        };
        response.Order = dto;
        return Results.Ok(response);
    }
}

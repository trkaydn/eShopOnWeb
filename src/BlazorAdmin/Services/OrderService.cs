﻿using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Interfaces;
using BlazorShared.Models;
using Microsoft.Extensions.Logging;

namespace BlazorAdmin.Services;

public class OrderService : IOrderService
{
    private readonly HttpService _httpService;
    private readonly ILogger<OrderService> _logger;

    public OrderService(HttpService httpService, ILogger<OrderService> logger)
    {
        _httpService = httpService;
        _logger = logger;
    }

    public Task<string> Delete(int id)
    {
        throw new System.NotImplementedException();
    }

    public async Task<Order> Edit(Order order)
    {
        var values = await _httpService.HttpPut<EditOrderResult>("orders", order);
        return values.Order;
    }

    public async Task<Order> GetById(int id)
    {
        var values = await _httpService.HttpGet<EditOrderResult>($"orders/{id}");
        return values.Order;
    }

    public async Task<List<Order>> List()
    {
        _logger.LogInformation("Fetching orders from API.");
        var values = await _httpService.HttpGet<PagedOrderResponse>($"orders");
        return values.Orders;
    }

    public async Task<List<Order>> ListPaged(int pageSize)
    {
        _logger.LogInformation("Fetching orders from API.");
        var values = await _httpService.HttpGet<PagedOrderResponse>($"orders");   
        return values.Orders;
    }
}
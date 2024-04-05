using System;
using System.Collections.Generic;

namespace BlazorShared.Models;
public class Order
{
    public int Id { get; set; }
    public string BuyerId { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    public OrderStatus Status { get; set; }
    public List<OrderItem> OrderItems { get; set; }
}

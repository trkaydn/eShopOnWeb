namespace BlazorShared.Models;
public class OrderItem
{
    public int Id { get; set; }
    public decimal UnitPrice { get;  set; }
    public int Units { get;  set; }
    public string ProductName { get;  set; }
}

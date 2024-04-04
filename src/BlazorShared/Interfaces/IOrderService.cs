using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorShared.Models;

namespace BlazorShared.Interfaces;
public interface IOrderService
{
    Task<Order> Edit(Order order);
    Task<string> Delete(int id);
    Task<Order> GetById(int id);
    Task<List<Order>> ListPaged(int pageSize);
    Task<List<Order>> List();
}

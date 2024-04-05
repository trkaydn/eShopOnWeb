using Microsoft.eShopWeb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.eShopWeb.PublicApi.OrderItemEndpoints;
using System.Net.Http.Headers;
using System.Net;

namespace PublicApiIntegrationTests.OrderEndpoints;

[TestClass]
public class OrderListEndpointTests
{
    [TestMethod]
    public async Task ReturnsOrderedOrderList()
    {
        var client = ProgramTest.NewClient;
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);
        var response = await client.GetAsync("/api/orders");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<ListOrderResponse>();

        // check orders exists
        Assert.IsTrue(model.Orders.Count > 0);

        // check orders order by desc
        var orderedOrders = model.Orders.OrderByDescending(o => o.OrderDate);
        var firstOrder = orderedOrders.First();
        var lastOrder = orderedOrders.Last();
        Assert.IsTrue(firstOrder.OrderDate >= lastOrder.OrderDate);
    }
}

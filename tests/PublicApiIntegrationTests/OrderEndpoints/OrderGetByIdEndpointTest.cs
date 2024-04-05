using Microsoft.eShopWeb;
using Microsoft.eShopWeb.PublicApi.OrderEndpoints;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PublicApiIntegrationTests.OrderEndpoints;


[TestClass]
public class OrderGetByIdEndpointTest
{
    [TestMethod]
    public async Task ReturnsItemGivenValidId()
    {
        //take 10th order
        //test Id and buyerId is equal
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

        var response = await client.GetAsync("api/orders/detail/10");
        response.EnsureSuccessStatusCode();
        var stringResponse = await response.Content.ReadAsStringAsync();
        var model = stringResponse.FromJson<GetByIdOrderResponse>();

        Assert.AreEqual(10, model!.Order.Id);
        Assert.AreEqual("admin@microsoft.com", model.Order.BuyerId);
    }

    [TestMethod]
    public async Task ReturnsNotFoundGivenInvalidId()
    {
        var adminToken = ApiTokenHelper.GetAdminUserToken();
        var client = ProgramTest.NewClient;
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

        var response = await client.GetAsync("api/orders/detail/0");
        Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
    }
}

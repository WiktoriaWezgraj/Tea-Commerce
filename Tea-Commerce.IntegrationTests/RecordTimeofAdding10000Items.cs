using Microsoft.AspNetCore.Mvc.Testing;
using System.Diagnostics;
using System.Net.Http.Json;
using Xunit.Abstractions;
using Tea.Domain.Models;
using FluentAssertions;

namespace Tea_Commerce.IntegrationTests;

public class RecordTimeOfAdding10000Items : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly ITestOutputHelper _output;

    public RecordTimeOfAdding10000Items(WebApplicationFactory<Program> factory, ITestOutputHelper output)
    {
        _client = factory.CreateClient();
        _output = output;
    }


    [Fact]
    public async Task Post_AddThousandsProductsAsync_ExceptedThousandsProducts()
    {
        var stopwatch = Stopwatch.StartNew();

        var tasks = new List<Task<HttpResponseMessage>>();
        for (int i = 1; i <= 10_000; i++)
        {
            var product = new Product { Name = $"Product {i}" };
            tasks.Add(_client.PostAsJsonAsync("/api/product", product));
        }

        await Task.WhenAll(tasks);
        stopwatch.Stop();

        var products = await _client.GetFromJsonAsync<List<Product>>("/api/product");
        products.Should().NotBeNull();
        products.Count.Should().BeGreaterThanOrEqualTo(10_000);

        _output.WriteLine($"Inserted 10,000 products asynchronously in {stopwatch.ElapsedMilliseconds} ms");
    }
}
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using Tea.Domain.Models;
using Tea.Domain.Repositories;
using FluentAssertions;

namespace Tea_Commerce.IntegrationTests;

public class ProductControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public ProductControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    private async Task ResetAndSeedDbAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dbContext.Products.RemoveRange(dbContext.Products);

        dbContext.Products.AddRange(
            new Product { Name = "Product1" },
            new Product { Name = "Product2" }
        );

        await dbContext.SaveChangesAsync();
    }

    [Fact]
    public async Task Get_ShouldReturnSeededProducts()
    {
        await ResetAndSeedDbAsync();

        var response = await _client.GetAsync("/api/product");
        response.EnsureSuccessStatusCode();

        var products = await response.Content.ReadFromJsonAsync<List<Product>>();
        products.Should().HaveCount(2);
    }

    [Fact]
    public async Task Post_ShouldAddProduct()
    {
        await ResetAndSeedDbAsync();

        var product = new Product { Name = "Product3" };

        var postResponse = await _client.PostAsJsonAsync("/api/product", product);
        postResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/product");
        var products = await getResponse.Content.ReadFromJsonAsync<List<Product>>();

        products.Should().Contain(p => p.Name == "Product3");
    }

    [Fact]
    public async Task Get_ById_ShouldReturnCorrectProduct()
    {
        await ResetAndSeedDbAsync();

        var allProducts = await _client.GetFromJsonAsync<List<Product>>("/api/product");
        var createdProduct = allProducts.Find(p => p.Name == "Product1");

        var getByIdResponse = await _client.GetAsync($"/api/product/{createdProduct.Id}");
        var productById = await getByIdResponse.Content.ReadFromJsonAsync<Product>();

        productById.Should().NotBeNull();
        productById.Name.Should().Be("Product1");
    }

    [Fact]
    public async Task Put_ShouldUpdateProductName()
    {
        await ResetAndSeedDbAsync();

        var products = await _client.GetFromJsonAsync<List<Product>>("/api/product");
        var toUpdate = products.Find(p => p.Name == "Product1");

        toUpdate.Name = "UpdatedProduct";

        var putResponse = await _client.PutAsJsonAsync($"/api/product/{toUpdate.Id}", toUpdate);
        putResponse.EnsureSuccessStatusCode();

        var updatedProduct = await _client.GetFromJsonAsync<Product>($"/api/product/{toUpdate.Id}");
        updatedProduct.Name.Should().Be("UpdatedProduct");
    }

    [Fact]
    public async Task Delete_ShouldSetProductDeletedTrue()
    {
        await ResetAndSeedDbAsync();

        var products = await _client.GetFromJsonAsync<List<Product>>("/api/product");
        var toDelete = products.Find(p => p.Name == "Product1");

        var deleteResponse = await _client.DeleteAsync($"/api/product/{toDelete.Id}");
        deleteResponse.EnsureSuccessStatusCode();

        var deleted = await _client.GetFromJsonAsync<Product>($"/api/product/{toDelete.Id}");
        deleted.Deleted.Should().BeTrue();
    }


    //dla patch
    [Fact]
    public async Task Add_AddProduct_ExpectedOneProduct()
    {
        await ResetAndSeedDbAsync();

        var product = new Product { Name = "PatchedProduct" };

        var patchResponse = await _client.PatchAsJsonAsync("/api/product", product);
        patchResponse.EnsureSuccessStatusCode();

        var getResponse = await _client.GetAsync("/api/product");
        var products = await getResponse.Content.ReadFromJsonAsync<List<Product>>();

        products.Should().Contain(p => p.Name == "PatchedProduct");
    }




    [Fact]
    public async Task RunMultipleTestsInParallel()
    {
        await ResetAndSeedDbAsync();

        var task1 = AddProductTest("ParallelProduct1");
        var task2 = AddProductTest("ParallelProduct2");
        var task3 = AddProductTest("ParallelProduct3");

        await Task.WhenAll(task1, task2, task3);

        var products = await _client.GetFromJsonAsync<List<Product>>("/api/product");
        products.Should().Contain(p => p.Name == "ParallelProduct1");
        products.Should().Contain(p => p.Name == "ParallelProduct2");
        products.Should().Contain(p => p.Name == "ParallelProduct3");
    }

    private async Task AddProductTest(string productName)
    {
        var product = new Product { Name = productName };
        var response = await _client.PostAsJsonAsync("/api/product", product);
        response.EnsureSuccessStatusCode();
    }

}
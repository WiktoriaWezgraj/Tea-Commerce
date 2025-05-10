using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using FluentAssertions;

namespace Tea_Commerce.IntegrationTests;

public class CreditCardControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CreditCardControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Get_ValidCard_ShouldReturnOk()
    {
        var validCard = "4111111111111111";

        var response = await _client.GetAsync($"/api/creditcard/{validCard}");
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Get_TooLongCard_ShouldReturn414()
    {
        var longCard = new string('4', 30);

        var response = await _client.GetAsync($"/api/creditcard/{longCard}");
        response.StatusCode.Should().Be((HttpStatusCode)414);
    }

    [Fact]
    public async Task Get_TooShortCard_ShouldReturn400()
    {
        var shortCard = "123";

        var response = await _client.GetAsync($"/api/creditcard/{shortCard}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Get_InvalidCard_ShouldReturn400()
    {
        var invalidCard = "abcdefghijk";

        var response = await _client.GetAsync($"/api/creditcard/{invalidCard}");
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Get_UnsupportedProvider_ShouldReturn406()
    {
        var unsupportedCard = "7777777777777777";

        var response = await _client.GetAsync($"/api/creditcard/{unsupportedCard}");
        response.StatusCode.Should().Be(HttpStatusCode.NotAcceptable);
    }
}
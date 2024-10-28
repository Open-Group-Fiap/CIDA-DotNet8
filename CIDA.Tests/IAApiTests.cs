using CIDA.Api;
using CIDA.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net.Http.Json;


namespace CIDA.Tests;

[Collection("Api Test Collection")]
public class IAApiTests
{

    private readonly HttpClient _client;

    public IAApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async void PostPrevisao_ReturnsPrevisao() {
        
        // Arrange
        var previsao = new PrevisaoRequest("Verão", "Sul");

        // Act
        var response = await _client.PostAsJsonAsync("/previsao", previsao);

        // Assert
        Assert.NotNull(response);
    }

    [Fact]
    public async void GetMetrics_ReturnsMetrics()
    {
        // Act
        var response = await _client.GetAsync("previsao/metrics");

        // Assert
        Assert.NotNull(response);
        
    }
}

using CIDA.Api;
using CIDA.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Net;
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
    public async void PostPrevisao_ReturnsBadRequest_WhenEstacaoIsInvalid()
    {
        // Arrange
        var previsao = new PrevisaoRequest("Verão2", "Sul");

        // Act
        var response = await _client.PostAsJsonAsync("/previsao", previsao);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Estação inválida. Use: Verão, Inverno, Primavera ou Outono", content.Trim('"'));
    }
    [Fact]
    public async void PostPrevisao_ReturnsBadRequest_WhenRegiaoIsInvalid()
    {
        // Arrange
        var previsao = new PrevisaoRequest("Verão", "Sul2");

        // Act
        var response = await _client.PostAsJsonAsync("/previsao", previsao);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("Região inválida. Use: Sul, Sudeste, Centro-Oeste, Norte ou Nordeste", content.Trim('"'));
    }

    [Fact]
    public async void PostPrevisao_ReturnsBadRequest_WhenSendRandomJson()
    {
        // Act
        var response = await _client.PostAsJsonAsync("/previsao", new {});

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
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

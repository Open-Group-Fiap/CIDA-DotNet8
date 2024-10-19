using System.Net;
using System.Net.Http.Json;
using CIDA.Api;
using CIDA.Api.Models;
using CIDA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CIDA.Tests;

public class InsightApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public InsightApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PutInsight_ReturnsInsight_WhenInsightExists()
    {
        // Arrange
        var insight = new InsightAddOrUpdateModel(
            1,
            1,
            "Descrição do insight"
        );

        // Act
        var response = await _client.PutAsJsonAsync("/insight/1", insight);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedInsight = await response.Content.ReadFromJsonAsync<Insight>();
        Assert.NotNull(updatedInsight);
        Assert.Equal(insight.Descricao, updatedInsight.Descricao);
    }
    
    [Fact]
    public async Task GetInsightById_ReturnsInsight_WhenInsightExists()
    {
        // Act
        var response = await _client.GetAsync("/insight/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var insight = await response.Content.ReadFromJsonAsync<Insight>();

        Assert.NotNull(insight);
    }
    
    [Fact]
    public async Task GetInsightByEmail_ReturnsInsight_WhenInsightExists()
    {
        // Act
        var response = await _client.GetAsync("/insight/example2@example.com");

        // Assert
        response.EnsureSuccessStatusCode();
        var insight = await response.Content.ReadFromJsonAsync<Insight>();

        Assert.NotNull(insight);
    }
    
    [Fact]
    public async Task DeleteInsight_ReturnsNoContent_WhenInsightExists()
    {
        // Act
        var response = await _client.DeleteAsync("/insight/2");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }    
    
    [Fact]
    public async Task GetInsightsByUsuarioEmail_ReturnsInsight_WhenInsightExists()
    {
        // Act
        var response = await _client.GetAsync("/insight/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var insight = await response.Content.ReadFromJsonAsync<InsightsListModel>();

        Assert.NotNull(insight);
    }
    
    [Fact]
    public async Task GetInsightsSearch_ReturnsInsight_WhenInsightExists()
    {
        // Act
        var response = await _client.GetAsync("/insight/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var insight = await response.Content.ReadFromJsonAsync<InsightsListModel>();

        Assert.NotNull(insight);
    }
    
}
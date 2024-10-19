using System.Net;
using System.Net.Http.Json;
using CIDA.Api;
using CIDA.Api.Models;
using CIDA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CIDA.Tests;

public class ResumoApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ResumoApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PutResumo_ReturnsResumo_WhenResumoExists()
    {
        // Act
        var response = await _client.PutAsJsonAsync("/resumo/1", new ResumoAddOrUpdateModel(
            1,
            "Descrição do resumo"
        ));

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedResume = await response.Content.ReadFromJsonAsync<Resumo>();
        Assert.NotNull(updatedResume);
        Assert.Equal("Descrição do resumo", updatedResume.Descricao);
    }

    [Fact]
    public async Task DeleteResumo_ReturnsNoContent_WhenResumoExists()
    {
        // Act
        var response = await _client.DeleteAsync("/resumo/2");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task GetResumosByUsuarioEmail_ReturnsResumo_WhenResumoExists()
    {
        // Act
        var response = await _client.GetAsync("/resumo/email/example@example.com");

        // Assert
        response.EnsureSuccessStatusCode();
        var resumo = await response.Content.ReadFromJsonAsync<ResumosListModel>();

        Assert.NotNull(resumo);
    }
    
    [Fact]
    public async Task GetResumosSearch_ReturnsResumo_WhenResumoExists()
    {
        // Act
        var response = await _client.GetAsync("/resumo/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var resumo = await response.Content.ReadFromJsonAsync<ResumosListModel>();

        Assert.NotNull(resumo);
    }

    [Fact]
    public async Task GetResumoById_ReturnsResumo_WhenResumoExists()
    {
        // Act
        var response = await _client.GetAsync("/resumo/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var resumo = await response.Content.ReadFromJsonAsync<Resumo>();

        Assert.NotNull(resumo);
    }

}
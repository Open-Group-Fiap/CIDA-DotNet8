using System.Net.Http.Json;
using CIDA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CIDA.Tests;
using Xunit;

public class UsuarioApiTests : IClassFixture<WebApplicationFactory<CIDA.Api.Program>>
{
    private readonly HttpClient _client;

    public UsuarioApiTests(WebApplicationFactory<CIDA.Api.Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task GetUsuarioById_ReturnsUsuario()
    {
        // Act
        var response = await _client.GetAsync("/usuario/21");

        // Assert
        response.EnsureSuccessStatusCode();
        var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

        Assert.NotNull(usuario);
    }
    
    
}
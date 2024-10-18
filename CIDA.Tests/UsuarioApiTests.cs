using System.Net;
using System.Net.Http.Json;
using CIDA.Api;
using CIDA.Api.Models;
using CIDA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CIDA.Tests;

public class UsuarioApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public UsuarioApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostUsuario_ReturnsUsuario()
    {
        // Arrange
        var usuario = new UsuarioAndAutenticacaoAddOrUpdateModel(
            "example3@example.com",
            "123456",
            "example",
            0,
            "111.111.111-22",
            "(12) 99999-9999");

        // Act
        var response = await _client.PostAsJsonAsync("/usuario", usuario);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdUsuario = await response.Content.ReadFromJsonAsync<Usuario>();
        Assert.NotNull(createdUsuario);
    }

    [Fact]
    public async Task PutUsuario_ReturnsUsuario_WhenUsuarioExists()
    {
        // Arrange
        var usuario = new UsuarioAndAutenticacaoAddOrUpdateModel(
            "example@example.com",
            "123456",
            "example",
            0,
            "000.000.000-00",
            "(21) 88888-8888");

        // Act
        var response = await _client.PutAsJsonAsync("/usuario/1", usuario);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedUsuario = await response.Content.ReadFromJsonAsync<Usuario>();
        Assert.NotNull(updatedUsuario);
        Assert.Equal(usuario.Telefone, updatedUsuario.Telefone);
    }


    [Fact]
    public async Task GetUsuarioById_ReturnsUsuario_WhenUsuarioExists()
    {
        // Act
        var response = await _client.GetAsync("/usuario/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

        Assert.NotNull(usuario);
    }

    [Fact]
    public async Task GetUsuarioByEmail_ReturnsUsuario_WhenUsuarioExists()
    {
        // Act
        var response = await _client.GetAsync("/usuario/email/example@example.com");

        // Assert
        response.EnsureSuccessStatusCode();
        var usuario = await response.Content.ReadFromJsonAsync<Usuario>();

        Assert.NotNull(usuario);
    }

    [Fact]
    public async Task DeleteUsuario_ReturnsNoContent_WhenUsuarioExists()
    {
        // Act
        var response = await _client.DeleteAsync("/usuario/2");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
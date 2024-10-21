using System.Net;
using System.Net.Http.Json;
using CIDA.Api;
using CIDA.Api.Models;
using CIDA.Domain.Entities;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CIDA.Tests;

public class ArquivoApiTests: IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ArquivoApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task PostArquivo_ReturnsArquivo()
    {
        
        // Arrange
        var link = "https://cidastore.blob.core.windows.net/teste-container/TechNova Relatório.pdf";
    
        using var httpClient = new HttpClient();
        var fileStream = await httpClient.GetStreamAsync(link);

        var content = new MultipartFormDataContent();
    
        content.Add(new StreamContent(fileStream), "file", "TechNova_Relatorio.pdf");

        // Act
        var response = await _client.PostAsync("/arquivo/idUsuario/1/upload", content);

        
        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var createdArquivo = await response.Content.ReadFromJsonAsync<Arquivo>();

        Assert.NotNull(createdArquivo);
    }

    [Fact]
    public async Task PutArquivo_ReturnsArquivo_WhenArquivoExists()
    {
        // Arrange
        var arquivo = new ArquivoUpdateModel(
            2
        );

        // Act
        var response = await _client.PutAsJsonAsync("/arquivo/1", arquivo);

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var updatedArquivo = await response.Content.ReadFromJsonAsync<Arquivo>();
        Assert.NotNull(updatedArquivo);
        Assert.Equal(arquivo.IdResumo, updatedArquivo.IdResumo);
    }
    
    [Fact]
    public async Task DeleteArquivo_ReturnsNoContent_WhenArquivoExists()
    {
        // Act
        var response = await _client.DeleteAsync("/arquivo/2");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task GetArquivoByEmail_ReturnsArquivo_WhenArquivoExists()
    {
        // Act
        var response = await _client.GetAsync("/arquivo/email/example@example.com/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var arquivo = await response.Content.ReadFromJsonAsync<ArquivosListModel>();

        Assert.NotNull(arquivo);
    }


    [Fact]
    public async Task GetArquivoById_ReturnsArquivo_WhenArquivoExists()
    {
        // Act
        var response = await _client.GetAsync("/arquivo/1");

        // Assert
        response.EnsureSuccessStatusCode();
        var arquivo = await response.Content.ReadFromJsonAsync<Arquivo>();

        Assert.NotNull(arquivo);
    }
    
    [Fact]
    public async Task GetArquivoByIdUsuario_ReturnsArquivo_WhenArquivoExists()
    {
        // Act
        var response = await _client.GetAsync("/arquivo/idUsuario/1/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var arquivo = await response.Content.ReadFromJsonAsync<ArquivosListModel>();

        Assert.NotNull(arquivo);
    }
    
    [Fact]
    public async Task GetArquivosSearch_ReturnsArquivo_WhenArquivoExists()
    {
        // Act
        var response = await _client.GetAsync("/arquivo/search");

        // Assert
        response.EnsureSuccessStatusCode();
        var arquivo = await response.Content.ReadFromJsonAsync<ArquivosListModel>();

        Assert.NotNull(arquivo);
    }

    [Fact]
    public async Task PostArquivo_ReturnsBadRequest_WhenSendRandomJson()
    {
        
        // Act
        var response = await _client.PostAsJsonAsync("/arquivo/idUsuario/1/upload", new { });
        
        // Assert
        Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
    }
    
}
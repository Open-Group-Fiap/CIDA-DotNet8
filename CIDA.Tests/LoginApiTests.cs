﻿using System.Net.Http.Json;
using CIDA.Api;
using CIDA.Api.Models;
using Microsoft.AspNetCore.Mvc.Testing;

namespace CIDA.Tests;

public class LoginApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public LoginApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task PostLogin_ReturnsUnauthorized_WhenEmailAndSenhaAreInvalid()
    {
        // Arrange
        var login = new LoginRequest(
            "example@example.com",
            "SenhaErrada"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/login", login);
        
        // Assert
        Assert.Equal("Unauthorized", response.StatusCode.ToString());
    }
    
    [Fact]
    public async Task PostLogin_ReturnsOk_WhenEmailAndSenhaAreValid()
    {
        // Arrange
        var login = new LoginRequest(
            "example@example.com",
            "123456"
        );

        // Act
        var response = await _client.PostAsJsonAsync("/login", login);

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("OK", response.StatusCode.ToString());
    }
    
}
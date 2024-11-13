using System.Text;
using System.Text.Json;
using Identity.Core.Application.DTOs;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using Identity.Core.Domain.Entities;
using TechTalk.SpecFlow;

namespace Identity.IntegrationTests.Drivers;

public class AuthenticationDriver
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly string _baseUrl;

    public AuthenticationDriver(ScenarioContext scenarioContext)
    {
        _configuration = scenarioContext.Get<IConfiguration>("Configuration");
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Get base URL from configuration or use default
        _baseUrl = "http://localhost:5036/api/auth";
    }

    public async Task<HttpResponseMessage> RegisterClient(RegisterClientRequest request)
    {
        var url = $"{_baseUrl}/register/client";
        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Registration failed with status {response.StatusCode}: {error}");
        }

        return response;
    }

    public async Task<HttpResponseMessage> RegisterDriver(RegisterDriverRequest request)
    {
        var url = $"{_baseUrl}/register/driver";
        var content = JsonSerializer.Serialize(request);

        return await _httpClient.PostAsync(
            new Uri(url),
            new StringContent(content, Encoding.UTF8, "application/json")
        );
    }

    public async Task<AuthenticationResult> Login(LoginRequest request)
    {
        var url = $"{_baseUrl}/login";
        var content = new StringContent(
            JsonSerializer.Serialize(request),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _httpClient.PostAsync(url, content);
        var responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Raw response: {responseContent}");

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Login failed: {responseContent}");
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        try
        {
            var result =  JsonSerializer.Deserialize<ApiResponse<AuthenticationResult>>(responseContent, options) 
                   ?? throw new Exception("Deserialization returned null");

            return result.Data;
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Deserialization error: {ex.Message}");
            Console.WriteLine($"Response content: {responseContent}");
            throw;
        }
    }

// Update models to match API response
    private class ApiResponse<T>
    {
        public T Data { get; set; }
    }
    
}
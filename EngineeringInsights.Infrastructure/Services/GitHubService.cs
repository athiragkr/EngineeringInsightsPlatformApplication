using AutoMapper.Configuration;
using Microsoft.Extensions.Configuration;
using EngineeringInsights.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace EngineeringInsights.Infrastructure.Services
{
    public class GitHubService
    {
        public readonly HttpClient _httpClient;
        public readonly IConfiguration _configuration;

        public GitHubService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<List<GitHubRepositoryResponse>> GetRepositoriesAsync()
        {
            var org = _configuration["GitHub:Organization"];
            var url = $"https://api.github.com/orgs/{org}/repos";

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("User-Agent", "EngineeringInsightsApp");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<GitHubRepositoryResponse>>(
                content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }
    }
}

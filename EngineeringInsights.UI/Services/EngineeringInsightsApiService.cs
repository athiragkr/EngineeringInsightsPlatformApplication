using EngineeringInsights.UI.Models;
using System.Net.Http.Json;

namespace EngineeringInsights.UI.Services
{
    public class EngineeringInsightsApiService
    {
        private readonly HttpClient _httpClient;

        public EngineeringInsightsApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DashboardSummary?>
            GetDashboardAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<DashboardSummary>
                    ("dashboard");
        }

        public async Task<List<Repository>?>
            GetRepositoriesAsync()
        {
            return await _httpClient
                .GetFromJsonAsync<List<Repository>>(
                    "repositories");
        }
    }
}

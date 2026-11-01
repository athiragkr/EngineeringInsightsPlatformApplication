using EngineeringInsights.Application.DTOs;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace EngineeringInsights.Infrastructure.Services
{
    public class RepositoryOwnershipService
    {
        private readonly IConfiguration _configuration;

        public RepositoryOwnershipService(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<string?> GetOwnerAsync(
            string repositoryName)
        {
            var path = Path.Combine(
               Directory.GetCurrentDirectory(),
               "Data",
                "repository-owners.json");

            if (!File.Exists(path))
                return null;

            var json = await File.ReadAllTextAsync(path);

            var mappings =
                JsonSerializer.Deserialize<
                    List<RepositoryOwnerMapping>>(json);

            return mappings?
                .FirstOrDefault(x =>
                    x.RepositoryName.Equals(
                        repositoryName,
                        StringComparison.OrdinalIgnoreCase))
                ?.OwnerTeam;
        }
    }
}

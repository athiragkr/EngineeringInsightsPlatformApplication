

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace EngineeringInsights.Infrastructure.Services
{
    public class RepositoryDataService
    {
        private readonly IConfiguration _configuration;

        public RepositoryDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SaveRepositoryAsync(string name, DateTime lastCommit)
        {
            using var connection =
                new SqlConnection(
                    _configuration.GetConnectionString(
                        "DefaultConnection"));

            await connection.OpenAsync();

            var sql =
                """
        INSERT INTO Repositories
        (
            Name,
            LastCommitDate,
            ContributorCount,
            RiskScore,
            RiskLevel
        )
        VALUES
        (
            @Name,
            @LastCommitDate,
            0,
            0,
            'Unknown'
        )
        """;

            using var command =
                new SqlCommand(sql, connection);

            command.Parameters.AddWithValue(
                "@Name", name);

            command.Parameters.AddWithValue(
                "@LastCommitDate", lastCommit);

            await command.ExecuteNonQueryAsync();
        }
    }
}

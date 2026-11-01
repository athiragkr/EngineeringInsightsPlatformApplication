using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EngineeringInsights.Infrastructure.Models
{
    public class GitHubRepositoryResponse
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("updated_at")]
        public DateTime UpdatedAt { get; set; }

    }
}

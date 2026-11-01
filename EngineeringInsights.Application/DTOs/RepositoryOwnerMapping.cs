using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace EngineeringInsights.Application.DTOs
{
    public class RepositoryOwnerMapping
    {
        [JsonPropertyName("repositoryName")]
        public string RepositoryName { get; set; } = string.Empty;
        [JsonPropertyName("ownerTeam")]
        public string OwnerTeam { get; set; } = string.Empty;
    }
}

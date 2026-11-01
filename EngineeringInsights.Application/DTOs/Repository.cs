using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringInsights.Application.DTOs
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? OwnerTeam { get; set; }
        public DateTime LastCommitDate { get; set; }
        public int ContributorCount { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public int RiskScore { get; set; }

        public List<string> RiskReasons { get; set; } = new List<string>(); 
    }
}

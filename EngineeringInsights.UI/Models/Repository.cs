namespace EngineeringInsights.UI.Models
{
    public class Repository
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? OwnerTeam { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public int RiskScore { get; set; }

        public List<string> RiskReasons { get; set; }
    }
}

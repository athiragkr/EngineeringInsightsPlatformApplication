using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringInsights.Application.DTOs
{
    public class DashboardSummary
    {
        public int TotalRepositories { get; set; }
        public int HighRisk { get; set; }
        public int MediumRisk{ get; set; }
        public int LowRisk { get; set; }

        public int UnownedRepositories { get; set; }
    }
}

using EngineeringInsights.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace EngineeringInsights.Infrastructure.Services
{
    public class RiskAssessmentService
    {
        public void CalculateRisk(Repository repository)
        {
            int score = 0;

            var daysInactive = (DateTime.Now - repository.LastCommitDate).TotalDays;

            if (daysInactive > 90)
            {
                score += 3;
                repository.RiskReasons.Add("Repository has been inactive for more than 90 days.");
            }
            else if (daysInactive > 30)
            {
                score += 2;
                repository.RiskReasons.Add("Repository has been inactive for more than 30 days.");
            }
            else if (daysInactive > 7)
            {
                score += 1;
                repository.RiskReasons.Add("Repository has been inactive for more than 7 days.");
            }

            if (repository.ContributorCount < 2)
            {
                score += 2;
                repository.RiskReasons.Add("Repository has less than 2 contributors.");
            }

            if (string.IsNullOrEmpty(repository.OwnerTeam))
            {
                score += 2;
                repository.RiskReasons.Add("Repository does not have an assigned owner team.");

                repository.RiskScore = score;
            }

            if (score >= 5)
            {
                repository.RiskLevel = "High";
            }
            else if (score >= 3)
            {
                repository.RiskLevel = "Medium";
            }
            else
            {
                repository.RiskLevel = "Low";
            }
        }
    }
}

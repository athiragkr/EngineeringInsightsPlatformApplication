using EngineeringInsights.Application.DTOs;
using EngineeringInsights.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringInsights.Tests
{
    public class RiskAssessmentServiceTests
    {
        private readonly RiskAssessmentService _service;

        public RiskAssessmentServiceTests()
        {
            _service = new RiskAssessmentService();
        }

        [Fact]
        public void HighRiskRepository_ShouldBeClassifiedAsHigh()
        {
            var repository = new Repository
            {
                Name = "legacy-api",
                LastCommitDate = DateTime.Now.AddDays(-180),
                ContributorCount = 0,
                OwnerTeam = null,
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Equal("High", repository.RiskLevel);
            Assert.True(repository.RiskScore >= 5);
        }

        [Fact]
        public void HealthyRepository_ShouldBeClassifiedAsLow()
        {
            var repository = new Repository
            {
                Name = "runtime",
                LastCommitDate = DateTime.Now,
                ContributorCount = 10,
                OwnerTeam = "Platform Team",
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Equal("Low", repository.RiskLevel);
        }

        [Fact]
        public void RepositoryWithoutOwner_ShouldContainOwnerReason()
        {
            var repository = new Repository
            {
                Name = "sample",
                LastCommitDate = DateTime.Now,
                ContributorCount = 5,
                OwnerTeam = null,
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Contains(
                "Repository does not have an assigned owner team.",
                repository.RiskReasons);
        }

        [Fact]
        public void InactiveRepository_ShouldContainInactiveReason()
        {
            var repository = new Repository
            {
                Name = "old-repo",
                LastCommitDate = DateTime.Now.AddDays(-120),
                ContributorCount = 5,
                OwnerTeam = "Data Team",
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Contains(
                "Repository has been inactive for more than 90 days.",
                repository.RiskReasons);
        }
    }
}

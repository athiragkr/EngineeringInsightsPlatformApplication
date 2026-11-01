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
        public void Repository_WithNoOwner_ShouldAddRiskPoints()
        {
            // Arrange
            var repository = new Repository
            {
                Name = "test-repo",
                LastCommitDate = DateTime.Now,
                ContributorCount = 5,
                OwnerTeam = null,
                RiskReasons = new List<string>()
            };

            // Act
            _service.CalculateRisk(repository);

            // Assert
            Assert.True(repository.RiskScore >= 2);
            Assert.Contains(
                "Repository does not have an assigned owner team.",
                repository.RiskReasons);
        }

        //Test High Risk Level Repository

        [Fact]
        public void Repository_WithNoOwner_AndNoContributors_ShouldBeHighRisk()
        {
            var repository = new Repository
            {
                Name = "legacy-api",
                LastCommitDate = DateTime.Now.AddDays(-120),
                ContributorCount = 0,
                OwnerTeam = null,
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Equal("High", repository.RiskLevel);
        }

        //Test Low Risk Level Repository

        [Fact]
        public void ActiveRepository_ShouldBeLowRisk()
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
        //Test Medium Risk Level Repository

        [Fact]
        public void Repository_WithLowContributors_ShouldBeMediumRisk()
        {
            var repository = new Repository
            {
                Name = "android-samples",
                LastCommitDate = DateTime.Now,
                ContributorCount = 1,
                OwnerTeam = "Team A",
                RiskReasons = new List<string>()
            };

            _service.CalculateRisk(repository);

            Assert.Equal("Medium", repository.RiskLevel);
        }
    }
}

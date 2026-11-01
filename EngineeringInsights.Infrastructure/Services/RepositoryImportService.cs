using EngineeringInsights.Application.DTOs;
using EngineeringInsights.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace EngineeringInsights.Infrastructure.Services
{
    public class RepositoryImportService
    {
        private readonly GitHubService _gitHubService;
        private readonly InsightsDbContext _dbContext;
        private readonly ILogger<RepositoryImportService> _logger;
        private readonly RiskAssessmentService _riskAssessmentService;
        private readonly RepositoryOwnershipService _repositoryOwnershipService;

        public RepositoryImportService(GitHubService gitHubService, InsightsDbContext dbContext, ILogger<RepositoryImportService> logger, 
            RiskAssessmentService riskAssessmentService, RepositoryOwnershipService repositoryOwnershipService)
        {
            _gitHubService = gitHubService;
            _dbContext = dbContext;
            _logger = logger;
            _riskAssessmentService = riskAssessmentService;
            _repositoryOwnershipService = repositoryOwnershipService;
        }

        public async Task ImportRepositoriesAsync()
        {
            try
            {
                var repositories = await _gitHubService.GetRepositoriesAsync();


                foreach (var repo in repositories)
                {

                    var owner = await _repositoryOwnershipService.GetOwnerAsync(repo.Name);
                    var exists =await _dbContext.Repositories
                            .AnyAsync(x => x.Name == repo.Name);

                    if (exists)
                        continue;

                    var repository = new Repository
                    {
                        Name = repo.Name,
                        LastCommitDate = repo.UpdatedAt,
                        ContributorCount = 0,
                        OwnerTeam = owner
                    };
                    _riskAssessmentService.CalculateRisk(repository);
                    _dbContext.Repositories.Add(repository);
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while importing repositories");
                throw;
            }

        }
    }
}

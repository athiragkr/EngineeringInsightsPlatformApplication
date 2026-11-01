using EngineeringInsights.Application.DTOs;
using EngineeringInsights.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringInsights.Controllers
{
    [Route("api/dashboard")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly InsightsDbContext _dbContext;
        public DashboardController(InsightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Get()
        {
            var response = new DashboardSummary
            {
                TotalRepositories = _dbContext.Repositories.Count(),
                HighRisk = _dbContext.Repositories.Count(r => r.RiskLevel == "High"),
                MediumRisk = _dbContext.Repositories.Count(r => r.RiskLevel == "Medium"),
                LowRisk = _dbContext.Repositories.Count(r => r.RiskLevel == "Low"),
                UnownedRepositories = _dbContext.Repositories.Count(r => string.IsNullOrWhiteSpace(r.OwnerTeam))
            };
            return Ok(response);
        }
    }
}

using EngineeringInsights.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EngineeringInsights.Controllers
{
    [Route("api/repositories")]
    [ApiController]
    public class RepositoriesController : ControllerBase
    {
        private readonly InsightsDbContext _dbContext;

        public RepositoriesController(InsightsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_dbContext.Repositories.ToList());
        }

        [HttpGet("high-risk")]
        public IActionResult GetHighRisk()
        {
            var repos = _dbContext.Repositories.Where(r => r.RiskLevel == "High").ToList();
            return Ok(repos);
        }


        [HttpGet("{id}/insights")]
        public async Task<IActionResult> GetInsights(int id)
        {
            var repository =
                await _dbContext.Repositories.FirstOrDefaultAsync(x => x.Id == id);

            if (repository == null)
            {
                return NotFound();
            }

            var recommendations = new List<string>();

            if (repository.ContributorCount < 2)
            {
                recommendations.Add("Increase contributor coverage");
            }

            if (string.IsNullOrWhiteSpace(repository.OwnerTeam))
            {
                recommendations.Add("Assign an owner team");
            }

            if ((DateTime.Now - repository.LastCommitDate).Days > 90)
            {
                recommendations.Add("Review repository activity");
            }

            return Ok(new
            {
                repository.Id,
                repository.Name,
                repository.RiskLevel,
                repository.RiskScore,
                repository.RiskReasons,
                Recommendations = recommendations
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var repository =
                await _dbContext.Repositories
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (repository == null)
                return NotFound();

            return Ok(repository);
        }

        [HttpGet("unowned")]
        public async Task<IActionResult> GetUnownedRepositories()
        {
            var repositories =
                await _dbContext.Repositories
                    .Where(x => string.IsNullOrWhiteSpace(x.OwnerTeam))
                    .ToListAsync();

            return Ok(repositories);
        }
    }
}

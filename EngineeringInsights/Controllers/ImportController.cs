using EngineeringInsights.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EngineeringInsights.Controllers
{
    [Route("api/import")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly RepositoryImportService _repositoryImportService;

        public ImportController(RepositoryImportService repositoryImportService, GitHubService gitHubService)
        {
            _repositoryImportService = repositoryImportService;
        }

        [HttpPost]
        public async Task<IActionResult> ImportRepositories()
        {
            try
            {
                await _repositoryImportService.ImportRepositoriesAsync();
                return Ok(new { message = "Repositories imported successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred while importing repositories.", details = ex.Message });
            }
        }
    }
}

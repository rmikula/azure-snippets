using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheckTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyHealthCheckController : ControllerBase
    {
        private readonly ILogger<MyHealthCheckController> _logger;
        private readonly HealthCheckService _healthCheckService;

        public MyHealthCheckController(ILogger<MyHealthCheckController> logger, HealthCheckService healthCheckService)
        {
            _logger = logger;
            _healthCheckService = healthCheckService;
        }

        [HttpGet]
        public async Task<ActionResult<HealthStatus>> GetHealthCheckStatus()
        {
            HealthReport status = await _healthCheckService.CheckHealthAsync();

            return Ok(status.Status);
        }
    }
}
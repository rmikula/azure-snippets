using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ILogger<DatabaseHealthCheck> _logger;

    public DatabaseHealthCheck(ILogger<DatabaseHealthCheck> logger)
    {
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        _logger.LogInformation("DatabaseHealthCheck health check executed");

        return Task.FromResult(new HealthCheckResult(HealthStatus.Unhealthy, "Some descritpion"));
    }
}
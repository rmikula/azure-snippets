using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HealthCheck.HealthChecks;

public class ReadinessHealthCheck : IHealthCheck
{
    private readonly ILogger<ReadinessHealthCheck> _logger;

    public ReadinessHealthCheck(ILogger<ReadinessHealthCheck> logger)
    {
        _logger = logger;
    }

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {

        _logger.LogInformation($"{nameof(ReadinessHealthCheck)} health check executed");
        
        return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, "Všystko gra!"));

    }
}
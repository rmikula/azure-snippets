using Microsoft.Extensions.Options;

namespace get_credentials.Options;

public class SomeServiceOptionsSetup : IConfigureOptions<SomeServiceOptions>
{
    private const string SectionName = "MyServiceConfig";
    private readonly ILogger<SomeServiceOptionsSetup> _logger;
    private readonly IConfiguration _configuration;

    public SomeServiceOptionsSetup(ILogger<SomeServiceOptionsSetup> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public void Configure(SomeServiceOptions options)
    {
        _logger.LogWarning("Configuring {Class}", nameof(SomeServiceOptions));
        _configuration.GetSection(SectionName).Bind(options);
    }
}
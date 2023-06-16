using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Security.KeyVault.Secrets;
using get_credentials.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;

namespace get_credentials.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeyVaultController : ControllerBase
    {
        private readonly ILogger<KeyVaultController> _logger;
        private readonly SecretClient _secretClient;
        private readonly IOptions<SomeServiceOptions> _options;

        public KeyVaultController(ILogger<KeyVaultController> logger, SecretClient secretClient, IOptions<SomeServiceOptions> options)
        {
            _logger = logger;
            _secretClient = secretClient;
            _options = options;
        }

        [HttpGet]
        public async Task<IActionResult> GetSecret(string name, CancellationToken cancellationToken)
        {
            _logger.LogInformation(message: "Searching for {Name}", name);
            _logger.LogWarning("Factor Config {Factor}", _options.Value.Factor);
            _logger.LogWarning("Factor Config {Url}", _options.Value.Url);
            

            try
            {
                var response = await _secretClient.GetSecretAsync(name: name, cancellationToken: cancellationToken);

                var httpResponse = response.GetRawResponse();

                if (response.HasValue)
                {
                    var value = response.Value.Value;
                    return Ok(value);
                }
            }
            catch (RequestFailedException e)
            {
                return StatusCode(e.Status, e.Message);
            }

            return NoContent();
        }
    }
}
using Asp.NetCore7AppSettings.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace Asp.NetCore7AppSettings.Controllers
{
    [Route("api/app-settings")]
    [ApiController]
    public class AppSettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IdentityConfiguration _identityConfigs;
        public AppSettingsController(IConfiguration configuration,
                                      IOptions<IdentityConfiguration> identityConfigAsIOptions)
        {
            _configuration = configuration;
            _identityConfigs = identityConfigAsIOptions.Value;
        }


        [HttpGet]
        public async Task<IActionResult> Get()=>Ok();

        [HttpGet("api-key")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetSimpleAsync()
        {
            var apiKey=_configuration.GetValue<string>("ApiKey");
            return Ok(new{apiKey });  
        }
        [HttpGet("api-key-guid")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetApiKeyAsGuid()
        {
            var apiKey = _configuration.GetValue<Guid>("ApiKey");
            return Ok(new { apiKey });
        }


        [HttpGet("identity-config-simple")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetIdentityConfigurationSimple()
        {
            var passwordMinLength = _configuration.GetValue<int>("IdentityConfiguration:PasswordConfiguration:MinLength");
            var userConfiguration = _configuration.GetSection("IdentityConfiguration:UserConfiguration");
            var defaultPassword = userConfiguration.GetValue<string>("DefaultPassword");
            return Ok(new { passwordMinLength, defaultPassword });
        }

        [HttpGet("identity-config-user-bind")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetIdentityUserConfigurationBinding()
        {
            UserConfiguration userConfiguration = new();
            _configuration.Bind("IdentityConfiguration:UserConfiguration", userConfiguration);

            return Ok( userConfiguration);
        }
        [HttpGet("identity-config-password-bind")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetIdentityPasswordConfigurationBinding()
        {
            PasswordConfiguration passwordConfiguration = new();
            _configuration.Bind("IdentityConfiguration:PasswordConfiguration", passwordConfiguration);

            return Ok(passwordConfiguration);
        }

        [HttpGet("identity-config-ioptions")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityConfiguration>> GetIdentityConfigByIoptins()
        {
            var configs =await Task.FromResult(_identityConfigs??new IdentityConfiguration());

            return Ok(configs);
        }

        [HttpGet("identity-config-ioptions2")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityConfiguration>> GetIdentityConfigByIoptins2(IOptions<IdentityConfiguration> identityConfigAsIOptions)
        {
            var idnConfigs = identityConfigAsIOptions.Value ?? new IdentityConfiguration();
            var configs = await Task.FromResult(idnConfigs);

            return Ok(configs);
        }




    }
}

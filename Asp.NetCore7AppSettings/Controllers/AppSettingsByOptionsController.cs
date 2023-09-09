using Asp.NetCore7AppSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace Asp.NetCore7AppSettings.Controllers
{
    [Route("api/app-settings-options")]
    [ApiController]
    public class AppSettingsByOptionsController : ControllerBase
    {
       

        [HttpGet("identity-config-ioptions")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityConfiguration>> GetIdentityConfigByIoptins(IOptions<IdentityConfiguration> identityConfigAsIOptions)
        {
            var idnConfigs = identityConfigAsIOptions.Value ?? new IdentityConfiguration();
            var configs = await Task.FromResult(idnConfigs);

            return Ok(configs);
        }

        [HttpGet("identity-config-ioptions2")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IdentityConfiguration>> GetIdentityConfigByIoptins2(IConfiguration _configuration)
        {
            var passwordMinLength = _configuration.GetValue<int>("IdentityConfiguration:PasswordConfiguration:MinLength");
            var userConfiguration = _configuration.GetSection("IdentityConfiguration:UserConfiguration");
            var defaultPassword = userConfiguration.GetValue<string>("DefaultPassword");
            return Ok(new { passwordMinLength, defaultPassword });
        }


    }
}

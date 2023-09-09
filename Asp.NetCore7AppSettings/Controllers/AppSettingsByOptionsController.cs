using Asp.NetCore7AppSettings.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mime;

namespace Asp.NetCore7AppSettings.Controllers
{
    [Route("api/app-settings-options")]
    [ApiController]
    public class AppSettingsByOptionsController : ControllerBase
    {
       

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
